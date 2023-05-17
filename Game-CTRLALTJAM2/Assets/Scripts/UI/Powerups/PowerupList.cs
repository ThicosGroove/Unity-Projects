using System.Collections.Generic;
using UnityEngine;

namespace UI.PowerupSystem
{
    public class PowerupList : MonoBehaviour
    {
        public List<GameObject> powerupList = new List<GameObject>();
        
        public void SpawnAllPowerUps(Vector2 pos, float areaOffset)
        {
            foreach (var powerUp in powerupList)
            {
                var PUDropChance = powerUp.GetComponent<PowerUpBehaviour>()._powerUp.dropChance;

                var rndDropChance = Random.Range(0, 100);

                if (PUDropChance > rndDropChance)
                {

                    GameObject newPowerUp = Instantiate(powerUp, pos, Quaternion.identity);
                }
            }
        }

        public void SpawnOnePowerUpEachtime(Vector2 pos, float areaOffset)
        {
            var rnd = Random.Range(0, powerupList.Count);

            GameObject newPowerUp = Instantiate(powerupList[rnd], pos, Quaternion.identity);
        }

        // Fazer o local do Spawn ser mais específico
        private Vector2 GetRandomPosition(Vector2 spawnerPos, float areaOffset)
        {
            return new Vector2(spawnerPos.x + Random.Range(areaOffset, areaOffset), spawnerPos.y + Random.Range(areaOffset, areaOffset));
        }
    }
}