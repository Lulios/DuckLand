using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class WhackPool : MonoBehaviour
{
    public List<GameObject> ducks;
    public List<GameObject> spawnPoints;
    public List<GameObject> freeSpawnPoints;

    public int score = 0;
    
    [SerializeField] private List<Mesh> meshes;
    [SerializeField] private XRBaseController controllerLeft;
    [SerializeField] private XRBaseController controllerRight;
    [SerializeField] private WhackPool whackPoolScript;
    [SerializeField] private float wantedY;
    [SerializeField] private GameObject duck;
    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        CR_running = false;
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
        ducks[^1].GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Count)];
        
        Mole duckScript = ducks[^1].GetComponent<Mole>();
        duckScript.controllerLeft = controllerLeft;
        duckScript.controllerRight = controllerRight;
        duckScript.whackPoolScript = whackPoolScript;
        duckScript.spawnPoint = freeSpawnPoints[random];

        duckScript.wantedPosition = new Vector3(freeSpawnPoints[random].transform.localPosition.x, wantedY,
            freeSpawnPoints[random].transform.localPosition.z);
        duckScript.ducksOut = 0;
        //duckScript.duckSpeed = 1;
        freeSpawnPoints.RemoveAt(random);
        
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }
}
