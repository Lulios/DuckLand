using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackPool : MonoBehaviour
{
    [SerializeField] private GameObject duck;
    private bool CR_running = false;
    public List<GameObject> ducks;
    public List<GameObject> spawnPoints;
    public List<GameObject> freeSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running && ducks.Count < 7)
        {
            StartCoroutine(duckSpawn());
        }
    }

    private IEnumerator duckSpawn()
    {
        CR_running = true;
        ducks.Add(Instantiate(duck, gameObject.transform));
        int random = Random.Range(0, freeSpawnPoints.Count);
        ducks[ducks.Count - 1].transform.position = freeSpawnPoints[random].transform.position;
        freeSpawnPoints.RemoveAt(random);

        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }
}
