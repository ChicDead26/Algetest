using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thermometers
{
    public class TriggerButton : MonoBehaviour
    {
        [SerializeField] private Thermometer thermometer;

        private void OnMouseDown()
        {
            thermometer.MeasureTemperature();
            print("a");
        }

        private void OnMouseUp()
        {
            thermometer.StopMeasuringTemperature();
            print("u");
        }
    }
}
