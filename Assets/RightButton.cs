using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thermometers
{
    public class RightButton : MonoBehaviour
    {
        [SerializeField] private Thermometer thermometer;

        private void OnMouseDown()
        {
            thermometer.ChangeToFahrenheit();
        }
    }
}
