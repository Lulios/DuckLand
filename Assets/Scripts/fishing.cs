using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fishing : MonoBehaviour
{
    public int score = 0;
    
    [SerializeField] private XRBaseController controllerLeft;
    [SerializeField] private XRBaseController controllerRight;
    private float _savedY = float.MinValue;
    private GameObject _duck;
    private Follower _duckScript;
    private bool _isFished = false;
    private Pool _poolScript;

    

    // Start is called before the first frame update
    void Start()
    {
        _poolScript = FindObjectOfType<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_savedY != float.MinValue && (_duck.transform.position.y - _savedY)  > 1.5)
        {
            for (int i = 0; i < _poolScript.ducks.Count; i++)
            {
                if (_poolScript.ducks[i] == _duck)
                {
                    _poolScript.ducks.RemoveAt(i);
                }
            }

            Destroy(_duck);
            _duck = null;
            _savedY = float.MinValue;
            _isFished = false;
            score++;
            gameObject.GetComponent<AudioSource>().Play(0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(CompareTag("Duck") && !_isFished)
        {
            col.gameObject.transform.SetParent(this.transform);
            _savedY = col.gameObject.transform.position.y;
            _duck = col.gameObject;
            _duck.GetComponents<AudioSource>()[Random.Range(0, _duck.GetComponents<AudioSource>().Length)].Play(0);
            controllerLeft.SendHapticImpulse(1f, 0.3f);
            controllerRight.SendHapticImpulse(1f, 0.3f);
            _duckScript = _duck.GetComponent<Follower>();
            _duckScript.enabled = !_duckScript.enabled;
            _isFished = true;
        }
    }
}
