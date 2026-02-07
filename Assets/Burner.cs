using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Burners
{
    public class Burner : MonoBehaviour
    {
        public GameObject CurrentObject { get; set; }
        public bool IsBurnerOn { get; set; }
        public Cube CurrentCube { get; set; }
        public bool IsBurnerOccupied { get; private set; }

        [SerializeField] private ParticleSystem fireParticle;
        [SerializeField] private GameObject fireGameObject;

        private Coroutine currentDelayCoroutine;
        private float timer;

        // Update is called once per frame
        void Start()
        {
            //StartCoroutine(StartUpDelay());
            double test = 25;
            for (int i = 0; i < 400; i++)
            {
                test = test + (200 - test) * (1 - Math.Pow(Math.E, -0.0025 * 1));
            }
            print(test + " a");
            for (int i = 0; i < 300; i++)//485
            {
                test = test + (25 - test) * (1 - Math.Pow(Math.E, -0.0008333 * 1));
            }
            print(test + " b");
            //test = 98.84;
            for (int i = 0; i < 100; i++)
            {
                test = test + (200 - test) * (1 - Math.Pow(Math.E, -0.0025 * 1));
            }
            print(test + " c");
        }

        private void OnTriggerEnter(Collider other)
        {
            print("called trigger");
            if (!IsBurnerOccupied)
            {
                if (CurrentObject != other.gameObject)
                {
                    PositionObject(other.gameObject);

                    if (IsBurnerOn)
                        HeatUp(CurrentCube);
                }
            }
            else
            {
                CurrentObject.GetComponent<Carriable>().ReturnToStartingPosition();
                CoolDown(CurrentCube);

                PositionObject(other.gameObject);

                if (IsBurnerOn)
                    HeatUp(CurrentCube);
            }
        }

        public void TurnOn()
        {
            if (currentDelayCoroutine != null)
                StopCoroutine(currentDelayCoroutine);

            currentDelayCoroutine = StartCoroutine(TurnOnDelay());
        }

        public void TurnOff()
        {
            if (currentDelayCoroutine != null)
                StopCoroutine(currentDelayCoroutine);

            currentDelayCoroutine = StartCoroutine(TurnOffDelay());
        }

        private void PositionObject(GameObject objectToPosition)
        {
            CurrentObject = objectToPosition;
            CurrentObject.transform.parent = transform;
            CurrentObject.transform.position = transform.position;
            CurrentObject.transform.rotation = Quaternion.identity;

            CurrentCube = CurrentObject.GetComponent<Cube>();
            CurrentObject.GetComponent<Carriable>().StopBeingCarried();

            IsBurnerOccupied = true;
        }

        public IEnumerator TurnOnDelay()
        {
            //timer = 0;
            fireParticle.Play();
            float timerMax = 3;

            while (timer < timerMax)
            {
                timer += Time.deltaTime;
                fireGameObject.transform.localScale = Vector3.one * (0.055f * (timer / 3));
                yield return null;
            }

            timer = 3;
            fireGameObject.transform.localScale = Vector3.one * 0.055f;

            IsBurnerOn = true;
            HeatUp(CurrentCube);
        }

        public IEnumerator TurnOffDelay()//
        {
            //timer = 3;
            float timerMax = 0;

            while (timer > timerMax)
            {
                timer -= Time.deltaTime;
                fireGameObject.transform.localScale = Vector3.one * (0.055f * (timer / 3));
                yield return null;
            }

            timer = 0;
            fireParticle.Stop();
            fireGameObject.transform.localScale = Vector3.zero;

            IsBurnerOn = false;
            CoolDown(CurrentCube);
        }

        public void HeatUp(Cube cubeToHeat)
        {
            print("called heat");

            //do particles and whatnot even if it isnt occupied

            if (IsBurnerOccupied)
            {
                if (cubeToHeat.CurrentTemperatureCoroutine != null)
                    StopCoroutine(cubeToHeat.CurrentTemperatureCoroutine);

                cubeToHeat.CurrentTemperatureCoroutine = StartCoroutine(HeatOverTime(cubeToHeat));
            }
        }

        private IEnumerator HeatOverTime(Cube cubeToHeat)
        {
            //float ttime = 0;
            while (true)
            {
                cubeToHeat.CurrentTemperatureInCelsius = cubeToHeat.CurrentTemperatureInCelsius + (200 - cubeToHeat.CurrentTemperatureInCelsius) * (1 - Math.Pow(Math.E, -cubeToHeat.MaterialConstantHeating * Time.deltaTime));
                //print(currentCube.currentTemperatureInCelsius);
                //ttime += Time.deltaTime;
                print("Ok heat");
                yield return null;
            }
        }

        public void CoolDown(Cube cubeToCool)
        {
            print("called cool");

            //stop particles and whatnot even if it isnt occupied

            if (IsBurnerOccupied)
            {
                if (cubeToCool.CurrentTemperatureCoroutine != null)
                    StopCoroutine(cubeToCool.CurrentTemperatureCoroutine);

                cubeToCool.CurrentTemperatureCoroutine = StartCoroutine(CoolOverTime(cubeToCool));
            }
        }

        private IEnumerator CoolOverTime(Cube cubeToCool)
        {
            while (true)
            {
                print("Ok cool");
                cubeToCool.CurrentTemperatureInCelsius = cubeToCool.CurrentTemperatureInCelsius + (25 - cubeToCool.CurrentTemperatureInCelsius) * (1 - Math.Pow(Math.E, -cubeToCool.MaterialConstantCooling * Time.deltaTime));
                yield return null;
            }
        }
    }
}