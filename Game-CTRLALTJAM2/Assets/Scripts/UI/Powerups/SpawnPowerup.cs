using UI.PowerupSystem;
using UnityEngine;
using UI.GameManagement;

namespace UI.PowerupSystem
{
    public class SpawnPowerup : MonoBehaviour
    {
        private PowerupList _powerUpList;
        private Vector2 _colliderArea;

        [SerializeField] private bool _spawnAllPowerUp;
        [SerializeField] private float _timeBetweenSpawnSeconds = 3f;
        [SerializeField] private float _spawnAreaOffset = 3f;

        private float i = 0;

        void Start()
        {
            _powerUpList = GetComponent<PowerupList>();
            _colliderArea = GetComponent<BoxCollider2D>().size;
        }

        void Update()
        {
            if (GameManager.Instance._state != GameState.GAMEPLAY) return;
    
            Spawn();
        }

        private void Spawn()
        {
            if (i < _timeBetweenSpawnSeconds)
                i += Time.deltaTime;
            else
            {
                if (_spawnAllPowerUp)
                {
                    _powerUpList.SpawnAllPowerUps(RandomArea(), _spawnAreaOffset);
                    i = 0;
                    return;
                }

                _powerUpList.SpawnOnePowerUpEachtime(RandomArea(), _spawnAreaOffset);
                i = 0;
            }
        }

        private Vector2 RandomArea()
        {
           return (Vector2)transform.position + Random.insideUnitCircle * _colliderArea;
        }
    }
}