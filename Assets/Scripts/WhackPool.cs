using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class WhackPool : MonoBehaviour
{
    public List<Mesh> Meshes;
    
    public XRBaseController ControllerLeft;
    public XRBaseController ControllerRight;
    public WhackPool WhackPoolScript;
    
    public List<GameObject> ducks;
    public List<GameObject> spawnPoints;
    public List<GameObject> freeSpawnPoints;
    public float WantedY;

    public int Score = 0;

    [SerializeField] private GameObject duck;
    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running && ducks.Count < 7 && freeSpawnPoints.Count != 0)
        {
            StartCoroutine(DuckSpawn());
        }
    }

    private IEnumerator DuckSpawn()
    {
        CR_running = true;
        ducks.Add(Instantiate(duck, gameObject.transform));
        int random = Random.Range(0, freeSpawnPoints.Count);
        ducks[^1].transform.position = freeSpawnPoints[random].transform.position;
        
        Mole duckScript = ducks[^1].GetComponent<Mole>();
        duckScript.ControllerLeft = ControllerLeft;
        duckScript.ControllerRight = ControllerRight;
        duckScript.WhackPoolScript = WhackPoolScript;
        duckScript.spawnPoint = freeSpawnPoints[random];

        duckScript.wantedPosition = new Vector3(freeSpawnPoints[random].transform.localPosition.x, WantedY,
            freeSpawnPoints[random].transform.localPosition.z);
        duckScript.ducksOut = 0;
        freeSpawnPoints.RemoveAt(random);
        
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }
}
