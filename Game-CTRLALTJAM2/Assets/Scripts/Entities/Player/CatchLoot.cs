using UnityEngine;
using Utilities;
using UI.GameManagement;
using UI.LootSystem;
using UI.PowerupSystem;
using UI.Audio;

namespace Entities.Player
{
    public class CatchLoot : MonoBehaviour
    {
        private PlayerPowerUpHandler _playerPowerUpHandler;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            _playerPowerUpHandler = GetComponent<PlayerPowerUpHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            WhatLootFound(other);

            if (!_playerPowerUpHandler.HasOnePowerUpActive) // Só consegue pegar 1 power up de cada vez
            {
                WhatPowerUpFound(other);
            }
        }

        //Switchs 
        private void WhatLootFound(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
                case Constants.BREAD:
                    BreadEffect(other);
                    break;
                case Constants.POPCORN:
                    PopcornEffect(other);
                    break;
                default:
                    break;
            }
        }
        private void WhatPowerUpFound(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
                case Constants.INCREASE_DASH:
                    DashEffect(other);
                    break;
                case Constants.INCREASE_LIFE:
                    IncLifeEffect(other);
                    break;
                case Constants.INCREASE_SPEED:
                    IncSpeedEffect(other);
                    break;
                case Constants.INCREASE_DAMAGE:
                    IncDamageEffect(other);
                    break;
                default:
                    break;
            }
        }


        // Loot Effects
        private void BreadEffect(Collider2D self) // restores 70% of life
        {
            if (_playerHealth.CurrentHealth == _playerHealth.MaxHealth) return;

            var values = self.gameObject.GetComponent<LootBehaviour>();
            var heal = values._valuePercentage;

            _playerHealth.RestoredLife(heal);
            Destroy(self.gameObject);
        }

        private void PopcornEffect(Collider2D self) // restores 40% of life
        {
            if (_playerHealth.CurrentHealth == _playerHealth.MaxHealth) return;

            var values = self.gameObject.GetComponent<LootBehaviour>();
            var heal = values._valuePercentage;

            _playerHealth.RestoredLife(heal);
            Destroy(self.gameObject);
        }

        // Power Ups Effects
        private void DashEffect(Collider2D self)
        {
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.powerups, transform.position);
            var values = self.gameObject.GetComponent<PowerUpBehaviour>();

            var coolDown = values._coolDownSeconds;
            var newSpeed = values._value;

            StartCoroutine(_playerPowerUpHandler.SettingDashPowerUp(coolDown, newSpeed));
            GameplayEvents.PowerUp(coolDown, Constants.PU_DASH_ICON);

            Destroy(self.gameObject);
        }

        private void IncLifeEffect(Collider2D self)
        {
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.powerups, transform.position);
            var values = self.gameObject.GetComponent<PowerUpBehaviour>();

            var coolDown = values._coolDownSeconds;
            var isOn = values._isOn;

            StartCoroutine(_playerPowerUpHandler.SettingLifePowerUp(coolDown, isOn));
            GameplayEvents.PowerUp(coolDown, Constants.PU_LIFE_ICON);

            Destroy(self.gameObject);
        }

        private void IncSpeedEffect(Collider2D self)
        {
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.powerups, transform.position);
            var values = self.gameObject.GetComponent<PowerUpBehaviour>();

            var coolDown = values._coolDownSeconds;
            var newSpeed = values._value;

            StartCoroutine(_playerPowerUpHandler.SettingSpeedPowerUp(coolDown, newSpeed));
            GameplayEvents.PowerUp(coolDown, Constants.PU_SPEED_ICON);

            Destroy(self.gameObject);
        }

        private void IncDamageEffect(Collider2D self)
        {
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.powerups, transform.position);
            var values = self.gameObject.GetComponent<PowerUpBehaviour>();

            var coolDown = values._coolDownSeconds;
            var newDamage = values._value;

            StartCoroutine(_playerPowerUpHandler.SettingDamagePowerUp(coolDown, newDamage));
            GameplayEvents.PowerUp(coolDown, Constants.PU_DAMAGE_ICON);

            Destroy(self.gameObject);
        }
    }
}