using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    
        private ParticleManager particleManager;

        private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private Stats stats;
    
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
            core.transform.parent.gameObject.SetActive(false);
            // Death Event
            EventManager.Instance.InvokeEvent(EventManager.GameEvent.GamePlay_PlayerDied);
        }

        private void OnEnable()
        {
            EventManager.Instance.AddListener(EventManager.GameEvent.Stats_OnValueZero_PhysicalHealth, Die);
        }

        private void OnDisable()
        {
            EventManager.Instance.AddListener(EventManager.GameEvent.Stats_OnValueZero_PhysicalHealth, Die);
        }
    }
}