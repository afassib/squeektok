using System;
using UnityEngine;

namespace Bardent.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {

        public EventManager.GameEvent OnValueChangeEvent;

        public EventManager.GameEvent OnCurrentValueZeroEvent;

        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);
                EventManager.Instance.InvokeEvent(OnValueChangeEvent);
                if (currentValue <= 0f)
                {
                    EventManager.Instance.InvokeEvent(OnCurrentValueZeroEvent);
                }
            }
        }
        
        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;

        public float GetPencentage()
        {
            if(CurrentValue < 0f) return 0f;
            if(CurrentValue >= MaxValue) return 1;
            return CurrentValue / MaxValue;
        }
    }
}