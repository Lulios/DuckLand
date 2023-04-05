using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mole : MonoBehaviour
{
    public XRBaseController controllerLeft;
    public XRBaseController controllerRight;
    public WhackPool whackPoolScript;
    public GameObject spawnPoint;
    public Vector3 wantedPosition;
    public int ducksOut;
    
    [SerializeField] private float duckSpeed;
    [SerializeField] private float duckCaughtSpeed;
    [SerializeField] private float yminToBeCaught;
    private float _defaultY;
    private Vector3 _duckPos;
    private Vector3 _defaultPos;
    private bool _duckCaught = false;

    void Start()
    {
        _defaultY = spawnPoint.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        _duckPos = transform.localPosition;
        _defaultPos = new Vector3(_duckPos.x,  _defaultY, _duckPos.z);
        if (_duckCaught)
        {
            if (Vector3.Distance(_defaultPos, _duckPos) > 0.001f)
            { 
                transform.localPosition = Vector3.MoveTowards(_duckPos, _defaultPos, duckCaughtSpeed * Time.deltaTime);
            }
            else
            {
                whackPoolScript.freeSpawnPoints.Add(spawnPoint);
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
                int index = whackPoolScript.ducks.IndexOf(gameObject);
                whackPoolScript.ducks.RemoveAt(index);
                whackPoolScript.freeSpawnPoints.Add(spawnPoint);
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
        if (transform.localPosition.y >= yminToBeCaught && _duckCaught == false && col.CompareTag("Player"))
        {
            //col.gameObject.transform.SetParent(this.transform);
            //savedY = col.gameObject.transform.position.y;
            GetComponents<AudioSource>()[Random.Range(0, GetComponents<AudioSource>().Length)].Play(0);
            controllerLeft.SendHapticImpulse(1f, 0.3f);
            controllerRight.SendHapticImpulse(1f, 0.3f);
            //duckScript = duck.GetComponent<Follower>();
            //duckScript.enabled = !duckScript.enabled;
            _duckCaught = true;
            int index = whackPoolScript.ducks.IndexOf(gameObject);
            whackPoolScript.ducks.RemoveAt(index);
            whackPoolScript.score++;
        }
    }
}
