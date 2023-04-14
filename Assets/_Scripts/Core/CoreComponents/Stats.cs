using System;
using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class Stats : CoreComponent
    {
        public event Action OnHealthZero;
        
        [SerializeField] private float maxHealth;
        private float currentHealth;
        protected override void Awake()
        {
            base.Awake();
            currentHealth = maxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                //TODO DIE()
                currentHealth = 0;
                
                OnHealthZero?.Invoke();
                
                Debug.Log(core.transform.parent.name + " Has Died");
            }
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);
        }
    }
}
