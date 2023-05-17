using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

namespace UI.Audio
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents instance { get; private set; }

        [field: Header("Game musics")]
        [field: SerializeField] public EventReference introMusic { get; private set; }
        [field: SerializeField] public EventReference menuMusic { get; private set; }
        [field: SerializeField] public EventReference gameplayMusic { get; private set; }
        [field: SerializeField] public EventReference bossMusic { get; private set; }

        [field: Header("Gameplay SFX")]
        [field: SerializeField] public EventReference playerGunshot { get; private set; }
        [field: SerializeField] public EventReference playerGunshotUpgraded { get; private set; }
        [field: SerializeField] public EventReference enemyGunshot { get; private set; }
        [field: SerializeField] public EventReference bossGunshot { get; private set; }
    [field: SerializeField] public EventReference enemyGotDamaged { get; private set; }
        [field: SerializeField] public EventReference powerups { get; private set; }
        [field: SerializeField] public EventReference victoryEnd { get; private set; }
        [field: SerializeField] public EventReference defeatEnd { get; private set; }

    [field: Header("Menu SFX")]
        [field: SerializeField] public EventReference menuPlay { get; private set; }
        [field: SerializeField] public EventReference menuConfirm { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("FMOD Events already exists!");
            }
            else
                instance = this;
        }
    }
}