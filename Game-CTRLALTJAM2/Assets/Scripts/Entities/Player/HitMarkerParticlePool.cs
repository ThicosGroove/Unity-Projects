using UnityEngine;
using UnityEngine.Pool;

public class HitMarkerParticlePool : MonoBehaviour
{
    private ObjectPool<HitMarkerParticlePool> _pool;
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

    public void SetPool(ObjectPool<HitMarkerParticlePool> pool)
    {
        _pool = pool;
    }
}
