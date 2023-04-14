using System;
using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;
        private ParticleManager particleManager;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

        private Stats stats;
        private new Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Stats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            Stats.OnHealthZero -= Die;
        }
    }
}
