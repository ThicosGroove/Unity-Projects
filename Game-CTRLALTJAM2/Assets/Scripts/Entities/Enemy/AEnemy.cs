using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.GameManagement;
using Entities.Player;
using UI.Audio;

namespace Entities.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class AEnemy : MonoBehaviour
    {
        [Space]
        [Header("Life Parameters")]
        [SerializeField] protected float maxHealth;
        [SerializeField] protected GameObject healthBar;
        [SerializeField] protected Image filledHealthtBar;
        [SerializeField] protected float _currentHealth;
        [Header("TEST")]
        [SerializeField] private bool _canDie = true;

        [Space]
        [Header("Movement Parameters")]
        [SerializeField] protected float _enemySpeed;
        protected Transform _target;
        protected Rigidbody2D _rb;

        [Header("Attack Parameters")]
        [SerializeField] protected float _minDistToAttack;
        protected bool _isPlayerInRange = false;
        protected Vector2 _distanceToPlayer;

        [Space]
        [Header("Particle Parameters")]
        [SerializeField] public EnemyParticleSpawner _particleSpawner;
        [SerializeField] public EnemyParticlePool _particlePrefab;
        [SerializeField] public Transform _bulletSpawn;
        [SerializeField] protected float _fireRate;
        protected float timer;

        [Space]
        [Header("Body Behaviour")]
        [SerializeField] private Transform _enemyBody;
        [SerializeField] private Transform _weaponHolder;
        protected SpriteRenderer _bodySP;


        private void Awake()
        {
            _currentHealth = maxHealth;
            healthBar.gameObject.SetActive(false);

            _rb = GetComponent<Rigidbody2D>();
            _bodySP = _enemyBody.GetComponent<SpriteRenderer>();

            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected virtual void Update()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY) return;

            
            EnemyLook(_target.position);
            FlipBody();
            VerifyRange();
            AttackBehaviour();
        }

        protected virtual void FixedUpdate()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY)
            {
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                return;
            }

            MovementTowardsPlayer();
        }


        // Movement
        protected virtual void EnemyLook(Vector3 target)
        {
            _distanceToPlayer = target - transform.position;
            float angle = (Mathf.Atan2(_distanceToPlayer.y, _distanceToPlayer.x) * Mathf.Rad2Deg) - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _weaponHolder.transform.rotation = rotation;
        }
        protected virtual void MovementTowardsPlayer()
        {
            if (_isPlayerInRange)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            _distanceToPlayer = _target.position - transform.position;
            var distNormilize = _distanceToPlayer.normalized;
            _rb.velocity = new Vector2(distNormilize.x * _enemySpeed, distNormilize.y * _enemySpeed);
        }

        protected virtual void FlipBody()
        {
            var flipDir = (_target.position.x < transform.position.x) ? (_bodySP.flipX = true) : (_bodySP.flipX = false);
        }

        // Attack
        protected virtual void VerifyRange()
        {
            var distX = Mathf.Abs(_distanceToPlayer.x);
            var distY = Mathf.Abs(_distanceToPlayer.y);

            _isPlayerInRange = (distX < _minDistToAttack && distY < _minDistToAttack) ? true : false;
        }
        protected virtual void AttackBehaviour()
        {
            if (!_isPlayerInRange) return;

            timer += Time.deltaTime;

            float nextTimeToFire = 1 / _fireRate;

            if (timer >= nextTimeToFire)
            {
                _particleSpawner._pool.Get();
                
                if(gameObject.CompareTag("Boss"))
                    GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.bossGunshot, transform.position);
                else
                    GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.enemyGunshot, transform.position);

                timer = 0;
            }
        }

        // Health
        private void OnParticleCollision(GameObject other)
        {
            if (!_canDie) return;

            var damage = other.GetComponent<ParticleDamageSystem>().Damage;
            healthBar.gameObject.SetActive(true);
            HealthBarFiller(damage);
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.enemyGotDamaged, transform.position);

        }

        protected virtual void LostHealth()
        {
            if (_currentHealth <= 0) Die();
        }

        protected virtual void HealthBarFiller(float damage)
        {
            _currentHealth -= damage;
            float fillAmountPercentage = _currentHealth / maxHealth;

            filledHealthtBar.fillAmount = Mathf.Lerp(filledHealthtBar.fillAmount, fillAmountPercentage, 1);

            LostHealth();
        }

        protected abstract void Die();
    }
}