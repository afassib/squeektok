using System;
using Bardent.CoreSystem.StatsSystem;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Stats : CoreComponent
    {
        [field: SerializeField] public Stat physicalHealth { get; private set; }
        [field: SerializeField] public Stat mentalHealth { get; private set; }
        [field: SerializeField] public Stat Poise { get; private set; }

       [SerializeField] private float poiseRecoveryRate;
        
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            physicalHealth.Init();
            mentalHealth.Init();
            Poise.Init();
        }

        private void Update()
        {
            if (Poise.CurrentValue.Equals(Poise.MaxValue))
                return;
            
            Poise.Increase(poiseRecoveryRate * Time.deltaTime);
        }
    }
}
