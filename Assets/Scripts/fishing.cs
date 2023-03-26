using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class fishing : MonoBehaviour
{
    public XRBaseController ControllerLeft;
    public XRBaseController ControllerRight;
    public int Score = 0;

    private float savedY = float.MinValue;
    private GameObject duck;
    private Follower duckScript;
    private bool isFished = false;
    private Pool poolScript;

    

    // Start is called before the first frame update
    void Start()
    {
        poolScript = FindObjectOfType<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (savedY != float.MinValue && (duck.transform.position.y - savedY)  > 1.5)
        {
            for (int i = 0; i < poolScript.ducks.Count; i++)
            {
                if (poolScript.ducks[i] == duck)
                {
                    poolScript.ducks.RemoveAt(i);
                }
            }

            Destroy(duck);
            duck = null;
            savedY = float.MinValue;
            isFished = false;
            Score++;
            gameObject.GetComponent<AudioSource>().Play(0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Duck" && !isFished)
        {
            col.gameObject.transform.SetParent(this.transform);
            savedY = col.gameObject.transform.position.y;
            duck = col.gameObject;
            duck.GetComponents<AudioSource>()[Random.Range(0, duck.GetComponents<AudioSource>().Length)].Play(0);
            ControllerLeft.SendHapticImpulse(1f, 0.3f);
            ControllerRight.SendHapticImpulse(1f, 0.3f);
            duckScript = duck.GetComponent<Follower>();
            duckScript.enabled = !duckScript.enabled;
            isFished = true;
        }
    }
}
