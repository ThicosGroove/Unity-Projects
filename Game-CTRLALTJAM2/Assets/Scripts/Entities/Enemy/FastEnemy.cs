using UnityEngine;
using UI.LootSystem;
using UI.GameManagement;
using Entities.Player;

namespace Entities.Enemy
{
    public class FastEnemy : AEnemy
    {
        protected override void Die()
        {
            GetComponent<LootBag>().SpawnLoot(transform.position);
            GameplayEvents.EnemyDeath(this.gameObject);
            Destroy(gameObject);
        }
    }
}
