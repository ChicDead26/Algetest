using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Burners
{
    public class Regulator : MonoBehaviour
    {
        [SerializeField] private Burner burner;
        [SerializeField] private Animator regulatorAnimator;
        private bool currentSpinState;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {
            currentSpinState = !currentSpinState;
            regulatorAnimator.SetBool("spin", currentSpinState);

            //burner.isBurnerOn = currentSpinState;

            if (currentSpinState)
            {
                //burner.HeatUp(burner.currentCube);
                burner.TurnOn();
            }
            else
            {
                //burner.CoolDown(burner.currentCube);
                burner.TurnOff();
            }
        }
    }
}