using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.GameManagement;


namespace Entities.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] Transform _playerBody;
        [SerializeField] Transform _playerParent;

        private SpriteRenderer _bodySP;

        private Camera _mainCamera;

        private Vector3 _direction;

        private void Start()
        {
            _mainCamera = Camera.main;
            _bodySP = _playerBody.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY) return;
          
            LookAtMouse();
            FlipBody();
        }

        private void LookAtMouse()
        {            
            Vector3 mouseWorldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _direction = mouseWorldPoint + (_mainCamera.transform.forward * 1f);
            var dir = _direction - this.transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }

        private void FlipBody()
        {
            var flipDir = (_direction.x < _playerParent.position.x) ? (_bodySP.flipX = true) : (_bodySP.flipX = false);
        }
    }
}