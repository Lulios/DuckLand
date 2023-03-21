using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Pool : MonoBehaviour
{
    [SerializeField] private List<PathCreator> pathsCreator;
    [SerializeField] private GameObject duck;
    private bool CR_running = false;
    public List<GameObject> ducks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running && ducks.Count < 20)
        {
            StartCoroutine(duckSpawn());
        }
    }

    private IEnumerator duckSpawn()
    {
        CR_running = true;
        ducks.Add(Instantiate(duck, gameObject.transform));
        ducks[ducks.Count - 1].GetComponent<Follower>().pathCreator = pathsCreator[Random.Range(0, 4)];
        
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }
}