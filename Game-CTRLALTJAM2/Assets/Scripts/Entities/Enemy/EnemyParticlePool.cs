using System.Collections;
using System.Collections.Generic;
using UI.Audio;
using UnityEngine;
using UnityEngine.Pool;
using Utilities;

namespace Entities.Enemy
{
    public class EnemyParticlePool : MonoBehaviour
    {
        private ObjectPool<EnemyParticlePool> _pool;
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();

            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            _pool.Release(this);
        }

        public void SetPool(ObjectPool<EnemyParticlePool> pool)
        {
            _pool = pool;
        }
    }
}

