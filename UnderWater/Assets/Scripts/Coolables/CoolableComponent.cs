using UnityEngine;

namespace Coolables
{
    public abstract class CoolableComponent : MonoBehaviour, ICoolable
    {
        protected int Heat;

        protected abstract int TemperatureFactor { get; }
        protected abstract int CriticalTemp { get; }

        public int GetEnergyDelta(int deltaTemp)
        {
            return Heat * TemperatureFactor;
        }

        public abstract int GetMaxHeatTransfere();

        protected bool IsHeatCritical()
        {
            return GetTemperature() > CriticalTemp;
        }

        public int GetTemperature()
        {
            var temp = 20 + Heat / TemperatureFactor;
            return temp;
        }

        public void ApplyHeatChange(int energy)
        {
            Heat = Mathf.Max(0, Heat + energy);
        }
    }
}