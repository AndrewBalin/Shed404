using System;
using UnityEngine;
using Fishing.BobberComponents;

namespace Fishing.Fish
{
    [RequireComponent(typeof(Collider))]
    public class FishingZone : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _rippleEffect;

        private Collider _collider;
    
        public event Action EnteredFishingZone;
    
        private void Start()
        {
            CreateRippleEffect();
        }
    
        private void CreateRippleEffect()
        {
            _rippleEffect = Instantiate(_rippleEffect, transform.position, Quaternion.identity);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bobber _))
            {
                EnteredFishingZone?.Invoke();
            }
        }
    }
}