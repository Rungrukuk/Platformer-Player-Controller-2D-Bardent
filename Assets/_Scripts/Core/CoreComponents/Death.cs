using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;
        private ParticleManager particleManager;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

        private Stats entityStats;
        private Stats EntityStats => entityStats ? entityStats : core.GetCoreComponent(ref entityStats);

        private void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            EntityStats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            EntityStats.OnHealthZero -= Die;
        }
    }
}
