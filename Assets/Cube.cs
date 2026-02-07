using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [field: SerializeField] public double MaterialConstantHeating { get; set; }
    [field: SerializeField] public double MaterialConstantCooling { get; set; }
    [field: SerializeField] public double InitialTemperatureInCelsius { get; set; }
    [field: SerializeField] public double CurrentTemperatureInCelsius { get; set; }
    public Coroutine CurrentTemperatureCoroutine { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        CurrentTemperatureInCelsius = InitialTemperatureInCelsius;
    }
}