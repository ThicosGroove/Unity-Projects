using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.LootSystem
{
    public class LootBag : MonoBehaviour
    {
        [SerializeField] private List<GameObject> lootList = new List<GameObject>();
        private List<GameObject> droppedItem = new List<GameObject>();

        public void SpawnLoot(Vector3 lootPos)
        {
            droppedItem = GetDropItens();
            Vector3 newPos = new Vector3(lootPos.x + Random.Range(-1, 1), lootPos.y + Random.Range(-1, 1), 0);

            if (droppedItem.Count == 0) return;

            var rnd = Random.Range(0, droppedItem.Count);
            GameObject newLoot = Instantiate(droppedItem[rnd], newPos, Quaternion.identity);
        }

        private List<GameObject> GetDropItens()
        {
            var rnd = Random.Range(0, 100);
            foreach (var item in lootList)
            {
                var dropChance = item.GetComponent<LootBehaviour>()._loot.dropChance;

                if (dropChance >= rnd)
                {
                    droppedItem.Add(item);
                }
            }

            return droppedItem;
        }
    }
}

//List<Loot> GetDroppedItem()
//{
//    int randNum = Random.Range(0, 100);
//    List<Loot> possibleItens = new List<Loot>();

//    foreach (Loot loot in lootList)
//    {
//        if (randNum <= loot.dropChance)
//        {
//            possibleItens.Add(loot);
//        }
//    }

//    if (possibleItens.Count > 0)
//    {
//        return possibleItens;
//    }
//    else
//    {
//        return null;
//    }
//}