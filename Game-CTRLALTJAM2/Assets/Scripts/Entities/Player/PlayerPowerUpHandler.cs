using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerPowerUpHandler : MonoBehaviour
    {

        private Player _player;
        private PlayerHealth _playerHealth;
        private PlayerShootBehaviour _playerShootBehaviour;

        private float _timer;
        private bool _hasOnePowerUpActive;

        public bool HasOnePowerUpActive { get => _hasOnePowerUpActive; set => _hasOnePowerUpActive = value; }

        private void Start()
        {
            _player = GetComponent<Player>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerShootBehaviour = GetComponentInChildren<PlayerShootBehaviour>();
        }

        public IEnumerator SettingDashPowerUp(float CoolDown, float newVelocity)
        {
            _hasOnePowerUpActive = true;
            _timer = 0;

            var normalDashSpeed = _player.DashSpeed;
            _player.DashSpeed *= newVelocity;

            while (CoolDown >= _timer)
            {
                _timer += Time.deltaTime;
                yield return null;
            }

            _player.DashSpeed = normalDashSpeed;
            _hasOnePowerUpActive = false;
        }

        public IEnumerator SettingSpeedPowerUp(float CoolDown, float newVelocity)
        {
            _hasOnePowerUpActive = true;
            _timer = 0;

            var normalSpeed = _player.PlayerSpeed;
            _player.PlayerSpeed *= newVelocity;

            while (CoolDown >= _timer)
            {
                _timer += Time.deltaTime;
                yield return null;
            }

            _player.PlayerSpeed = normalSpeed;
            _hasOnePowerUpActive = false;
        }

        public IEnumerator SettingLifePowerUp(float CoolDown, bool isOn)
        {
            _hasOnePowerUpActive = true;
            _timer = 0;

            _playerHealth.CanDie = isOn;

            while (CoolDown >= _timer)
            {
                _timer += Time.deltaTime;
                yield return null;
            }

            _playerHealth.CanDie = !_playerHealth.CanDie;
            _hasOnePowerUpActive = false;
        }

        public IEnumerator SettingDamagePowerUp(float CoolDown, float newDamage)
        {
            _hasOnePowerUpActive = true;
            _timer = 0;

            var normalDamage = _playerShootBehaviour.Damage;
            _playerShootBehaviour.Damage = newDamage;

            while (CoolDown >= _timer)
            {
                _timer += Time.deltaTime;
                yield return null;
            }

            _playerShootBehaviour.Damage = normalDamage;
            _hasOnePowerUpActive = false;
        }
    }
}
