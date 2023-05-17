using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Player
{
    public class HitMarkerParticleSpawner : MonoBehaviour
    {
        [SerializeField] private HitMarkerParticlePool _hitMarketParticlePool;
        [SerializeField] private Transform _hitPoint;

        private ObjectPool<HitMarkerParticlePool> _pool;


        private void Start()
        {
            _pool = new ObjectPool<HitMarkerParticlePool>(CreateParticles, OnTakeParticleFromPool, OnReturnParticleToPool, OnDestroyParticles, true, 200, 350);
        }

        private void OnParticleCollision(GameObject other)
        {
            _hitPoint = other.transform;
            _pool.Get();
        }

        private HitMarkerParticlePool CreateParticles()
        {
            HitMarkerParticlePool particles = Instantiate(_hitMarketParticlePool, _hitPoint.position, _hitPoint.rotation);
            particles.transform.rotation = Quaternion.Euler(-90, -90, _hitPoint.rotation.z);
            particles.SetPool(_pool);
            return particles;
        }

        private void OnTakeParticleFromPool(HitMarkerParticlePool particles)
        {
            particles.transform.position = _hitPoint.position;
            particles.transform.rotation = Quaternion.Euler(-90, -90, _hitPoint.rotation.z);

            particles.gameObject.SetActive(true);
        }

        private void OnReturnParticleToPool(HitMarkerParticlePool particles)
        {
            particles.gameObject.SetActive(false);
        }

        private void OnDestroyParticles(HitMarkerParticlePool particles)
        {
            Destroy(particles.gameObject);
        }
    }
}


