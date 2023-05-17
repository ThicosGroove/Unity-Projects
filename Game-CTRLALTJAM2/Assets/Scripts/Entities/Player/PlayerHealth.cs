using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using UI.GameManagement;
using Cinemachine;


namespace Entities.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private Image filledHealthtBar;
        [SerializeField] private float _currentHealth;

        [Space]
        [Tooltip("Take No Damage When Is On. Used On Life PowerUp.")]
        [SerializeField] private bool _canDie = true;

        private List<ParticleCollisionEvent> collisionEvents;

        private CinemachineImpulseSource _impulseSource;


        // Getters And Setters
        public float MaxHealth { get => _maxHealth; }
        public float CurrentHealth { get => _currentHealth;}
        public bool CanDie { get => _canDie; set => _canDie = value; }

        private void Start()
        {
            _currentHealth = _maxHealth;

            collisionEvents = new List<ParticleCollisionEvent>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!_canDie) return;

            CameraShakeManager.Instance.CameraShake(_impulseSource);
            LostLife(1);
        }

        private void LostLife(float damage)
        {
            _currentHealth -= damage;

            float fillAmountPercentage = _currentHealth / _maxHealth;

            filledHealthtBar.fillAmount = Mathf.Lerp(filledHealthtBar.fillAmount, fillAmountPercentage, 1);

            if (_currentHealth <= 0) Die();
        }

        public void RestoredLife(float heal)
        {
            var totalHealing = _maxHealth * heal;
            if ((_currentHealth + totalHealing) >= _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth += totalHealing;

            float fillAmountPercentage = _currentHealth / _maxHealth;
            filledHealthtBar.fillAmount = Mathf.Lerp(filledHealthtBar.fillAmount, fillAmountPercentage, 1);
        }

        private void Die()
        {
            GameplayEvents.GameOver();
            //Debug.LogWarning("GAME OVER");
        }
    }
}

