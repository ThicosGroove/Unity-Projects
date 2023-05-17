using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class ParticleDamageSystem : MonoBehaviour
    {
        [SerializeField] private float _damage;
        public float Damage { get => _damage; set => _damage = value; }
    }
}
