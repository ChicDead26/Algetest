using Burners;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Thermometers
{
    public class Thermometer : MonoBehaviour
    {
        public TemperatureType CurrentTemperatureType { get; private set; }

        [SerializeField] private Burner burner;
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Carriable thermometerCarriable;
        [SerializeField] private Rigidbody thermometerRigidbody;
        [SerializeField] private Collider thermometerCollider;
        [SerializeField] private Collider[] buttonsColliders;

        [SerializeField] private GameObject visor;
        [SerializeField] private LineRenderer laser;
        [SerializeField] private TMP_Text temperatureTypeUI;
        [SerializeField] private TMP_Text temperatureDisplayUI;
        [SerializeField] private TMP_Text maxTemperatureDisplayUI;

        private float rayDistance = 10;
        private float displayTemperatureCelsius;
        private float maxTemperatureCelsius;
        private Coroutine thermometerCoroutine;
        private GameObject currentCubeGameObject;
        private Cube currentCube;

        // Start is called before the first frame update
        void Start()
        {
            visor.SetActive(false);
            laser.enabled = false;

            //ResetValues();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.DrawRay(rayOrigin.position, rayOrigin.forward);
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, rayDistance, mask))
            {
                //print("saw");
                currentCubeGameObject = hit.transform.gameObject;
                currentCube = currentCubeGameObject.GetComponent<Cube>();

                if (burner.IsBurnerOccupied && currentCubeGameObject == burner.CurrentObject)
                {
                    thermometerRigidbody.constraints = RigidbodyConstraints.None;
                    thermometerCollider.enabled = false;

                    transform.parent = burner.transform;
                    transform.position = new Vector3(5.58793688f, 1.137f, -5.263f);
                    transform.eulerAngles = new Vector3(0, -90, 0);

                    thermometerCarriable.StopBeingCarried();
                }
            }
        }

        public void MeasureTemperature()
        {
            if (thermometerCoroutine != null)
                StopCoroutine(thermometerCoroutine);

            if (!visor.activeInHierarchy)
                ResetValues();

            thermometerCoroutine = StartCoroutine(StartMeasuring());

            SetLaserPositions();
            laser.enabled = true;
        }

        private IEnumerator StartMeasuring()
        {
            visor.SetActive(true);

            yield return new WaitForSeconds(0.9f);
            System.Random rnd = new();

            while (true)
            {
                float twoPercentOfCurrentTemperature = ((float)currentCube.CurrentTemperatureInCelsius / 100f) * 2f;
                float errorMargin = RandomDecimal(rnd, -twoPercentOfCurrentTemperature, twoPercentOfCurrentTemperature);
                float temperatureWithErrorMargin = (float)currentCube.CurrentTemperatureInCelsius + errorMargin;

                displayTemperatureCelsius = Mathf.Clamp(temperatureWithErrorMargin, 0f, 50f);

                if (maxTemperatureCelsius < displayTemperatureCelsius)
                {
                    maxTemperatureCelsius = displayTemperatureCelsius;
                }

                switch (CurrentTemperatureType)
                {
                    case TemperatureType.Celsius:
                        temperatureTypeUI.text = "ºC";

                        //if (temperatureWithErrorMargin < 0)
                        //{
                        //    //temperatureDisplayUI.text = "00.0";
                        //    displayTemperature = 0;
                        //}
                        //else if (temperatureWithErrorMargin > 50)
                        //{
                        //    //temperatureDisplayUI.text = "50.0";
                        //    displayTemperature = 50;
                        //}
                        //else
                        //{
                        //    displayTemperature = temperatureWithErrorMargin;
                        //}

                        temperatureDisplayUI.text = displayTemperatureCelsius.ToString("00.0");

                        //if (maxTemperatureCelsius < displayTemperature)
                        //{
                        //    maxTemperatureCelsius = displayTemperature;
                        //}

                        maxTemperatureDisplayUI.text = $"MAX {maxTemperatureCelsius.ToString("00.0")}";

                        break;

                    case TemperatureType.Fahrenheit:

                        temperatureTypeUI.text = "ºF";
                        temperatureDisplayUI.text = CelsiusToFahrenheit(displayTemperatureCelsius).ToString("00.0");
                        maxTemperatureDisplayUI.text = $"MAX {CelsiusToFahrenheit(maxTemperatureCelsius).ToString("00.0")}";

                        break;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        public void StopMeasuringTemperature()
        {
            StopCoroutine(thermometerCoroutine);
            thermometerCoroutine = StartCoroutine(StopMeasuring());

            laser.enabled = false;
        }

        private IEnumerator StopMeasuring()
        {
            yield return new WaitForSeconds(15f);

            visor.SetActive(false);//hide all thermometer UI
        }

        public void ChangeToFahrenheit()
        {
            if (CurrentTemperatureType == TemperatureType.Celsius)
            {
                CurrentTemperatureType = TemperatureType.Fahrenheit;

                temperatureTypeUI.text = "ºF";
                temperatureDisplayUI.text = CelsiusToFahrenheit(displayTemperatureCelsius).ToString("00.0");
                maxTemperatureDisplayUI.text = $"MAX {CelsiusToFahrenheit(maxTemperatureCelsius).ToString("00.0")}";
            }
        }

        public void ChangeToCelsius()
        {
            if (CurrentTemperatureType == TemperatureType.Fahrenheit)
            {
                CurrentTemperatureType = TemperatureType.Celsius;

                temperatureTypeUI.text = "ºC";
                temperatureDisplayUI.text = displayTemperatureCelsius.ToString("00.0");
                maxTemperatureDisplayUI.text = $"MAX {maxTemperatureCelsius.ToString("00.0")}";
            }
        }

        public void ResetMaxTemperature()
        {
            maxTemperatureCelsius = 0;

            switch (CurrentTemperatureType)
            {
                case TemperatureType.Celsius:
                    maxTemperatureDisplayUI.text = $"MAX {maxTemperatureCelsius.ToString("00.0")}";
                    break;
                case TemperatureType.Fahrenheit:
                    maxTemperatureDisplayUI.text = $"MAX {CelsiusToFahrenheit(maxTemperatureCelsius).ToString("00.0")}";
                    break;
            }
        }

        private void ResetValues()
        {
            CurrentTemperatureType = TemperatureType.Celsius;

            temperatureTypeUI.text = "ºC";
            temperatureDisplayUI.text = "00.0";
            maxTemperatureDisplayUI.text = "MAX 00.0";
        }

        private void SetLaserPositions()
        {
            laser.SetPosition(0, rayOrigin.position);
            laser.SetPosition(1, rayOrigin.position + (rayOrigin.forward * 0.1f));// * 0.1f);
        }

        private float CelsiusToFahrenheit(float temperatureInCelsius)
        {
            return (temperatureInCelsius * 9f / 5f) + 32f;
        }

        public float RandomDecimal(System.Random random, float minValue, float maxValue)
        {
            return Mathf.Lerp(minValue, maxValue, (float)random.NextDouble());
        }
    }

    public enum TemperatureType
    {
        Celsius,
        Fahrenheit
    }
}
