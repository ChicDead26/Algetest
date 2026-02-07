using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thermometers
{
    public class LeftButton : MonoBehaviour
    {
        [SerializeField] private Thermometer thermometer;

        private void OnMouseDown()
        {
            thermometer.ChangeToCelsius();
            print("a");
        }
    }
}
