using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Pool : MonoBehaviour
{
    public List<GameObject> ducks;
    [SerializeField] private List<Mesh> meshes;
    [SerializeField] private List<PathCreator> pathsCreator;
    [SerializeField] private GameObject duck;
    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running && ducks.Count < 20)
        {
            StartCoroutine(DuckSpawn());
        }
    }

    private IEnumerator DuckSpawn()
    {
        CR_running = true;
        ducks.Add(Instantiate(duck, gameObject.transform));
        ducks[^1].GetComponent<Follower>().pathCreator = pathsCreator[Random.Range(0, 4)];
        ducks[^1].GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Count)];
        
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        CR_running = false;
    }
}