using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mole : MonoBehaviour
{
    public XRBaseController ControllerLeft;
    public XRBaseController ControllerRight;
    private GameObject duck;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Duck")
        {
            //col.gameObject.transform.SetParent(this.transform);
            //savedY = col.gameObject.transform.position.y;
            duck = col.gameObject;
            duck.GetComponents<AudioSource>()[Random.Range(0, duck.GetComponents<AudioSource>().Length)].Play(0);
            ControllerLeft.SendHapticImpulse(1f, 0.3f);
            ControllerRight.SendHapticImpulse(1f, 0.3f);
            //duckScript = duck.GetComponent<Follower>();
            //duckScript.enabled = !duckScript.enabled;
            Destroy(duck);
        }
    }
}
