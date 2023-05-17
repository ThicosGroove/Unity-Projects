using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UI.GameManagement;
using UI.Audio;

namespace Entities.Player
{
    public class PlayerShootBehaviour : MonoBehaviour
    {
        [SerializeField] public ParticlePool _particlePool;
        [SerializeField] public Transform _bulletSpawn;
        [SerializeField] private float _fireRate;

        private ParticleSpawner _particleSpawner;
        private InputControl _input;

        private float timer;
        private bool _isShooting;

        private float _damage = 1;

        // Getters and Setters 
        public float Damage { get => _damage; set => _damage = value; }

        private void Awake()
        {
            _input = new InputControl();
        }

        private void Start()
        {
            _particleSpawner = GetComponent<ParticleSpawner>();
        }

        private void OnEnable()
        {
            _input.Player.Shoot.performed += OnShootThisFrame;
            _input.Player.Shoot.canceled += OnStopShooting;
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Shoot.performed -= OnShootThisFrame;
            _input.Player.Shoot.canceled -= OnStopShooting;
        }

        void Update()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY) return;

            ShootBehaviour();
            SetUpDamage();
        }

        private void OnShootThisFrame(InputAction.CallbackContext context) => _isShooting = true;

        private void OnStopShooting(InputAction.CallbackContext context) => _isShooting = false;

        private void ShootBehaviour()
        {
            if (!_isShooting) return;

            timer += Time.deltaTime;

            float nextTimeToFire = 1 / _fireRate;

            if (timer >= nextTimeToFire)
            {
                _particleSpawner._pool.Get();
                GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.playerGunshot, transform.position);
                timer = 0;
            }
        }

        private void SetUpDamage()
        {
            _particleSpawner.Damage = _damage;
        }
    }

}

