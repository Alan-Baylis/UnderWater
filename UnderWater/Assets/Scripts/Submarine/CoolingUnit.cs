using System.Linq;
using Coolables;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class CoolingUnit : CoolableComponent
    {
        public Text HeatText;

        private void FixedUpdate()
        {
            
            ApplyHeatChange(-GetMaxHeatTransfere()/5);
            Ui();

            var coolables = GetComponents<ICoolable>().Where(coolable => coolable != this).ToList();
            var maxHeat = 0;
            foreach (var coolable in coolables)
            {
                maxHeat += TargetHeatTransfer(coolable);
            }


            var factor = 0f;
            if (maxHeat > 0)
            {
                factor = (float) GetMaxHeatTransfere() / maxHeat;
            }
            factor = Mathf.Min(factor, 1f);

            foreach (var coolable in coolables)
            {
                var energy = (int)Mathf.Min(TargetHeatTransfer(coolable) * factor);

                ApplyHeatChange(energy);
                coolable.ApplyHeatChange(-energy);
            }
        }

        private void Ui()
        {
            if (HeatText == null)
            {
                return;
            }

            var temp = GetTemperature();
            HeatText.text = string.Format("CoolingUnit: {0}°C", temp);
            if (IsHeatCritical())
                HeatText.color = Color.red;
            else
                HeatText.color = Color.black;
        }

        private int TargetHeatTransfer(ICoolable coolable)
        {
            var deltaTemp = Mathf.Max(coolable.GetTemperature() - GetTemperature(), 0);
            var targetHeatTransfer = Mathf.Min(coolable.GetMaxHeatTransfere(), coolable.GetEnergyDelta(deltaTemp));
            return targetHeatTransfer;
        }

        protected override int TemperatureFactor
        {
            get { return 300; }
        }

        protected override int CriticalTemp
        {
            get { return 300; }
        }

        public override int GetMaxHeatTransfere()
        {
            return 100;
        }
    }
}