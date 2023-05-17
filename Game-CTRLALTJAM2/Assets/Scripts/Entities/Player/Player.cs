using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UI.GameManagement;

namespace Entities.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Player Normal Movements Parameters")]
        [SerializeField] private float _playerSpeed;

        [Header("Player Dash Parameters")]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashCoolDownTimeSeconds;
        [SerializeField] private bool _isDashing;
        [SerializeField] private bool _canDash = true;
        private float _dashTotalDistance = 0.2f;
        private float _timer;
        private bool _dashInput = false;

        private Vector2 _movement;

        private Rigidbody2D _rb;
        private InputControl _input;

        // Getters And Setters
        public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
        public float DashCoolDownTimeSeconds { get => _dashCoolDownTimeSeconds; set => _dashCoolDownTimeSeconds = value; }
        public float DashSpeed { get => _dashSpeed; set => _dashSpeed = value; }

        private void Awake()
        {
            _input = new InputControl();
            _rb = GetComponent<Rigidbody2D>();        
        }

        private void OnEnable()
        {
            _input.Player.Dash.started += OnDash;
            _input.Player.Dash.canceled += OnDashCanceled;

            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Dash.started -= OnDash;
            _input.Player.Dash.canceled -= OnDashCanceled;
        }

        private void Update()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY) return;

            GetPlayerMovement();

            if (!_canDash)         
                DashCoolDown();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY)
            {
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                return;
            }

            MovePlayer();

            if (_dashInput && _canDash)
                Dash();
        }

        private void GetPlayerMovement() => _movement = _input.Player.Movement.ReadValue<Vector2>().normalized;

        private void OnDash(InputAction.CallbackContext context) => _dashInput = true;
        private void OnDashCanceled(InputAction.CallbackContext context) => _dashInput = false;

        private void MovePlayer()
        {
            if (_isDashing) return;

            _rb.velocity = new Vector2(_movement.x * _playerSpeed, _movement.y * _playerSpeed);
        }

        private void Dash()
        {
            _canDash = false;
            _isDashing = true;
            GameplayEvents.Dash(_dashCoolDownTimeSeconds);

            StartCoroutine(DashBehaviour());
        }

        private void DashCoolDown()
        {
            _timer += Time.deltaTime;
            if (_timer >= _dashCoolDownTimeSeconds)
            {
                _canDash = true;
                _timer = 0;
                return;
            }
        }

        private IEnumerator DashBehaviour()
        {
            _rb.velocity = Vector2.zero;

            var dashForce = new Vector2(_movement.x * _dashSpeed, _movement.y * _dashSpeed);
            _rb.AddForce(dashForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(_dashTotalDistance);
            _isDashing = false;
        }
    }
}