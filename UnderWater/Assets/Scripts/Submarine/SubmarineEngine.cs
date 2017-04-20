using Controller;
using Coolables;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class SubmarineEngine : CoolableComponent, IControllable
    {
        private Vector2 _motorControll;
        private Rigidbody2D _rigidbody2D;

        public Text MotorHeatText;
        public int MotorPower;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Heat = 10;
            ControllerManager.Target = gameObject;
        }

        private void FixedUpdate()
        {
            Ui();
            var baseLift = 800 * Vector2.up;


            var baseFactor = 1;
            if (IsHeatCritical())
                baseFactor /= 2;

            var lift = baseFactor * 800 * _motorControll.y;
            var motorInput = baseFactor * _motorControll.x * MotorPower;

            var force = Vector2.right * motorInput + Vector2.up * lift;

            Heat += (int) force.magnitude / 20;
            _rigidbody2D.AddForce(force + baseLift);
        }

        private void Ui()
        {
            if (MotorHeatText == null)
            {
                return;
            }

            var temp = GetTemperature();
            MotorHeatText.text = string.Format("Engine: {0}°C", temp);
            if (IsHeatCritical())
                MotorHeatText.color = Color.red;
            else
                MotorHeatText.color = Color.black;
        }

        protected override int TemperatureFactor
        {
            get { return 100; }
        }

        protected override int CriticalTemp
        {
            get { return 200; }
        }

        public override int GetMaxHeatTransfere()
        {
            var fastCooling = Mathf.Max((int)Mathf.Log10(Heat), 0);
            return 22 + 2 * fastCooling;
        }

        public void UpdateAxis(Vector2 controllVector)
        {
            _motorControll = controllVector;
        }
    }
}