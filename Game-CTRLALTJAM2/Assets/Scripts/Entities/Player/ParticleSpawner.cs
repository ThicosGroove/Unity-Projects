using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Player
{
    public class ParticleSpawner : MonoBehaviour
    {
        public ObjectPool<ParticlePool> _pool;

        private List<ParticleDamageSystem> _particleDamageList = new List<ParticleDamageSystem>();

        private PlayerShootBehaviour _playerShootBehaviour;
        private ParticleDamageSystem _particleDamageSystem;

        private float _damage;
        public float Damage { get => _damage; set => _damage = value; }

        private void Start()
        {
            _playerShootBehaviour = GetComponent<PlayerShootBehaviour>();
            _pool = new ObjectPool<ParticlePool>(CreateParticles, OnTakeParticleFromPool, OnReturnParticleToPool, OnDestroyParticles, true, 200, 350);
        }

        private void Update()
        {
            SetUpDamage();
        }

        private ParticlePool CreateParticles()
        {
            ParticlePool particles = Instantiate(_playerShootBehaviour._particlePool, _playerShootBehaviour._bulletSpawn.position, _playerShootBehaviour._bulletSpawn.rotation);
            particles.transform.SetParent(_playerShootBehaviour._bulletSpawn);
            _particleDamageSystem = particles.GetComponent<ParticleDamageSystem>();
            _particleDamageList.Add(_particleDamageSystem);
            
            particles.SetPool(_pool);

            return particles;
        }

        private void OnTakeParticleFromPool(ParticlePool particles)
        {
            particles.transform.position = _playerShootBehaviour._bulletSpawn.position;
            particles.transform.rotation = _playerShootBehaviour._bulletSpawn.rotation;

            particles.gameObject.SetActive(true);
        }

        private void OnReturnParticleToPool(ParticlePool particles)
        {
            particles.gameObject.SetActive(false);
        }

        private void OnDestroyParticles(ParticlePool particles)
        {
            Destroy(particles.gameObject);
        }

        private void SetUpDamage()
        {
            foreach (var particle in _particleDamageList)
            {
                particle.Damage = _damage;
            }
        }
    }

}

