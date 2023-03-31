using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhackPool : MonoBehaviour
{
    public List<GameObject> ducks;
    public List<GameObject> spawnPoints;
    public List<GameObject> freeSpawnPoints;
    public List<Vector3> wantedPositions = new List<Vector3>();
    public float wantedY;
    public float duckSpeed;
    [SerializeField] private GameObject duck;
    private Vector3 _duckPos;
    private float _defaultY;
    private Vector3 _defaultPos;
    private bool CR_running = false;
    private List<int> ducksOut = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        _defaultY = spawnPoints[0].transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running && ducks.Count < 7)
        {
            StartCoroutine(DuckSpawn());
        }

        for (int i = 0; i < ducks.Count; i++)
        {
            _duckPos = ducks[i].transform.localPosition;
            _defaultPos = new Vector3(wantedPositions[i].x, _defaultY, wantedPositions[i].z);
            if (ducksOut[i] == 0 && Vector3.Distance(wantedPositions[i], _duckPos) > 0.001f)
            { 
                ducks[i].transform.localPosition = Vector3.MoveTowards(_duckPos, wantedPositions[i], duckSpeed * Time.deltaTime);
            }
            else if (ducksOut[i] == 1 && Vector3.Distance(_defaultPos, _duckPos) > 0.001f)
            {
                ducks[i].transform.localPosition = Vector3.MoveTowards(_duckPos, _defaultPos, duckSpeed * Time.deltaTime);
            }
            else if (ducksOut[i] == 0 || ducksOut[i] == 1)
            {
                StartCoroutine(Delayed_DO_Change(ducksOut[i],i));
                ducksOut[i] = -1;
            }
        }
    }

    private IEnumerator DuckSpawn()
    {
        CR_running = true;
        ducks.Add(Instantiate(duck, gameObject.transform));
        int random = Random.Range(0, freeSpawnPoints.Count);
        ducks[^1].transform.position = freeSpawnPoints[random].transform.position;
        wantedPositions.Add(new Vector3(freeSpawnPoints[random].transform.localPosition.x, wantedY,
            freeSpawnPoints[random].transform.localPosition.z));
        ducksOut.Add(0);
        freeSpawnPoints.RemoveAt(random);
        

        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }

    private IEnumerator Delayed_DO_Change(int input, int index)
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        if (input == 0)
        {
            ducksOut[index] = 1;
        }
        else
        {
            ducksOut[index] = 0;
        }
    }
}
