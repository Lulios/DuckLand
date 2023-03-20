using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class fishing : MonoBehaviour
{
    public XRBaseController ControllerLeft;
    public XRBaseController ControllerRight;
    private float savedY = float.MinValue;
    private GameObject duck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (savedY != float.MinValue && (duck.transform.position.y - savedY)  > 1.5)
        {
            Destroy(duck);
            duck = null;
            savedY = float.MinValue;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Duck")
        {
            col.gameObject.transform.SetParent(this.transform);
            savedY = col.gameObject.transform.position.y;
            duck = col.gameObject;
            ControllerLeft.SendHapticImpulse(1f, 0.3f);
            ControllerRight.SendHapticImpulse(1f, 0.3f);
        }
    }
}
