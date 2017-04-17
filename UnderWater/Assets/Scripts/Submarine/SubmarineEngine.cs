﻿using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class SubmarineEngine : MonoBehaviour
    {
        private float _motorControll;
        private float _playerlift;
        private Rigidbody2D _rigidbody2D;

        private int heat;
        public int MotorPower;
        public Text MotorHeatText;
        private int _criticalHeat;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            heat = 10;
            _criticalHeat = 10000;
        }

        private void FixedUpdate()
        {
            Cooling();
            var baseLift = 800 * Vector2.up;


            var baseFactor = 1;
            if (heat > _criticalHeat)
            {
                baseFactor /= 2;
            }

            var lift = baseFactor * 800 * _playerlift;
            var motorInput = baseFactor * _motorControll * MotorPower;

            var force = Vector2.right * motorInput + Vector2.up * lift;

            heat += (int) force.magnitude/25;
            _rigidbody2D.AddForce(force + baseLift);
        }

        private void Cooling()
        {
            var temp = GetTemperature();
            MotorHeatText.text = string.Format("Temp: {0}°C", temp);
            if (heat > _criticalHeat)
            {
                MotorHeatText.color = Color.red;
            }
            else
            {
                MotorHeatText.color = Color.black;
            }
            var fastCooling = Mathf.Max((int) Mathf.Log10(heat), 0);
            var cooling = 10 + fastCooling;
            heat = Mathf.Max(0, heat - cooling);
        }

        private int GetTemperature()
        {
            var temp = 20 + heat/100;
            return temp;
        }

        private void Update()
        {
            _motorControll = Input.GetAxis("Horizontal");
            _playerlift = Input.GetAxis("Vertical");
            Debug.Log(_motorControll + "/" + _playerlift);
        }
    }
}