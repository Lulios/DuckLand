using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mole : MonoBehaviour
{
    public List<Mesh> Meshes;
    public XRBaseController ControllerLeft;
    public XRBaseController ControllerRight;
    public WhackPool WhackPoolScript;
    public GameObject spawnPoint;
    public float duckSpeed;
    public float duckCaughtSpeed;
    public float YminToBeCaught;
    public Vector3 wantedPosition;
    public int ducksOut;
    public float _defaultY;
    private Vector3 _duckPos;
    private Vector3 _defaultPos;
    private bool duckCaught = false;

    void Start()
    {
        _defaultY = spawnPoint.transform.localPosition.y;
        GetComponent<MeshFilter>().mesh = Meshes[Random.Range(0, Meshes.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        _duckPos = transform.localPosition;
        _defaultPos = new Vector3(transform.localPosition.x,  _defaultY, transform.localPosition.z);
        if (duckCaught)
        {
            if (Vector3.Distance(_defaultPos, _duckPos) > 0.001f)
            {
            transform.localPosition = Vector3.MoveTowards(_duckPos, _defaultPos, duckCaughtSpeed * Time.deltaTime);
            }
            else
            {
                WhackPoolScript.freeSpawnPoints.Add(spawnPoint);
                Destroy(gameObject);
            }
        }
        else
        {
            if (ducksOut == 0 && Vector3.Distance(wantedPosition, _duckPos) > 0.001f)
            {
                transform.localPosition = Vector3.MoveTowards(_duckPos, wantedPosition, duckSpeed * Time.deltaTime);
            }
            else if (ducksOut == 1 && Vector3.Distance(_defaultPos, _duckPos) > 0.001f)
            {
                transform.localPosition = Vector3.MoveTowards(_duckPos, _defaultPos, duckSpeed * Time.deltaTime);
            }
            else if (ducksOut == 0)
            {
                StartCoroutine(Delayed_DO_Change(ducksOut));
                ducksOut = -1;
            }
            else if (ducksOut == 1)
            {
                int index = WhackPoolScript.ducks.IndexOf(gameObject);
                WhackPoolScript.ducks.RemoveAt(index);
                WhackPoolScript.freeSpawnPoints.Add(spawnPoint);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Delayed_DO_Change(int input)
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        if (input == 0)
        {
            ducksOut = 1;
        }
        else
        {
            ducksOut = 0;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (transform.localPosition.y >= YminToBeCaught && duckCaught == false && col.tag == "Player")
        {
            //col.gameObject.transform.SetParent(this.transform);
            //savedY = col.gameObject.transform.position.y;
            GetComponents<AudioSource>()[Random.Range(0, GetComponents<AudioSource>().Length)].Play(0);
            ControllerLeft.SendHapticImpulse(1f, 0.3f);
            ControllerRight.SendHapticImpulse(1f, 0.3f);
            //duckScript = duck.GetComponent<Follower>();
            //duckScript.enabled = !duckScript.enabled;
            duckCaught = true;
            int index = WhackPoolScript.ducks.IndexOf(gameObject);
            WhackPoolScript.ducks.RemoveAt(index);
            WhackPoolScript.Score++;
        }
    }
}
