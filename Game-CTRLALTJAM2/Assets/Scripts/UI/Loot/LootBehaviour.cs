using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.LootSystem
{
    public class LootBehaviour : MonoBehaviour
    {
        [SerializeField] public Loot _loot;

        [Space]
        [SerializeField] private float _maxTimeAliveSeconds;

        [HideInInspector] public SpriteRenderer _lootSprite;
        [HideInInspector] public string _lootName;
        [HideInInspector] public int _dropChance;
        [HideInInspector] public float _valuePercentage;


        private void Start()
        {
            _lootSprite = GetComponent<SpriteRenderer>();

            _lootSprite.sprite = _loot.Sprite;
            _lootName = _loot.Name;
            gameObject.tag = _loot.tag;
            _dropChance = _loot.dropChance;
            _valuePercentage = _loot.valuePercentage;

            Destroy(gameObject, _maxTimeAliveSeconds);
        }
    }
}

