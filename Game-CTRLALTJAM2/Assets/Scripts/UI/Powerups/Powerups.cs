using UnityEngine;

namespace UI.PowerupSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PowerUp")]
    public class Powerups : ScriptableObject
    {
        public Sprite Sprite;
        public PowerUpTypes type;
        public string tag;
        public int dropChance;

        public float value;
        public float coolDownSeconds;
        public bool isOn;
    }

    public enum PowerUpTypes
    {
        DAMAGE,
        LIFE,
        SPEED,
        DASH_SPEED
    }
}