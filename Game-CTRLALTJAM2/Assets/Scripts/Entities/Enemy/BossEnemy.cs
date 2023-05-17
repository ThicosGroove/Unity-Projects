using UnityEngine;
using UI.GameManagement;
using UI.Audio;

namespace Entities.Enemy
{
    public class BossEnemy : AEnemy
    {
        protected override void Die()
        {
            GameObject.FindObjectOfType<AudioManager>().CleanUp();
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.victoryEnd, Vector3.zero);
            GameplayEvents.WinGame();
            Destroy(gameObject);
        }
    }
}