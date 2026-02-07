using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thermometers
{
    public class MiddleButton : MonoBehaviour
    {
        [SerializeField] private Thermometer thermometer;

        private void OnMouseDown()
        {
            thermometer.ResetMaxTemperature();
        }
    }
}
