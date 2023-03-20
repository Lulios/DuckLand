using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ActivateRayForGrab : MonoBehaviour
{
    public GameObject leftRay;
    public GameObject rightRay;

    public XRDirectInteractor leftGrab;
    public XRDirectInteractor rightGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftRay.SetActive(leftGrab.interactablesSelected.Count == 0);
        rightRay.SetActive(rightGrab.interactablesSelected.Count == 0);
    }
}
