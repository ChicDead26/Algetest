using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : MonoBehaviour
{
    //now, yes, this totally should be an interface, possibly implemented by both cube and thermometer
    public bool IsBeingCarried { get; private set; }
    public bool IsPlaced { get; set; }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform propsTransform;
    [SerializeField] private Rigidbody thisRigidbody;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }


    private void OnMouseDown()
    {
        if (!IsBeingCarried && !IsPlaced)
        {
            BeginBeingCarried();
        }
    }

    public void BeginBeingCarried()
    {
        transform.SetParent(cameraTransform);
        thisRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        IsBeingCarried = true;
    }

    public void StopBeingCarried()
    {
        IsPlaced = true;

        thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        IsBeingCarried = false;
    }

    public void ReturnToStartingPosition()
    {
        transform.SetParent(propsTransform);
        transform.position = initialPosition;
        thisRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        IsPlaced = false;
        IsBeingCarried = false;
    }
}
