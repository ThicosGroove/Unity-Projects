using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Entities.Enemy
{
    public class EnemyParticleSpawner : MonoBehaviour
    {
        public ObjectPool<EnemyParticlePool> _pool;

        private AEnemy _myEnemy;
        private EnemyParticlePool particles = null;

        private void Start()
        {
            _myEnemy = GetComponentInParent<AEnemy>();
            _pool = new ObjectPool<EnemyParticlePool>(CreateParticles, OnTakeParticleFromPool, OnReturnParticleToPool, OnDestroyParticles, true, 200, 350);
        }

        private EnemyParticlePool CreateParticles()
        {
 
                particles = Instantiate(_myEnemy._particlePrefab, _myEnemy._bulletSpawn.position, _myEnemy._bulletSpawn.rotation);
                particles.transform.SetParent(_myEnemy._bulletSpawn);
                particles.SetPool(_pool);
            
            return particles;
        }

        private void OnTakeParticleFromPool(EnemyParticlePool particles)
        {

                particles.transform.position = _myEnemy._bulletSpawn.position;
                particles.transform.rotation = _myEnemy._bulletSpawn.rotation;

                particles.gameObject.SetActive(true);
            
        }

        private void OnReturnParticleToPool(EnemyParticlePool particles)
        {
            particles.gameObject.SetActive(false);
        }

        private void OnDestroyParticles(EnemyParticlePool particles)
        {
            Destroy(particles.gameObject);
        }
    }
}

