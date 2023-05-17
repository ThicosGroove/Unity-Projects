using UnityEngine;
using UI.GameManagement;
using UI.LootSystem;
using Entities.Player;

namespace Entities.Enemy
{
    public class BasicEnemy : AEnemy
    { 
        protected override void Die()
        {
            GetComponent<LootBag>().SpawnLoot(transform.position);
            GameplayEvents.EnemyDeath(this.gameObject);
            Destroy(gameObject);
        }
    }
}

