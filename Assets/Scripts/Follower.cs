using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public List<Mesh> Meshes;
    public PathCreator pathCreator;
    public float speed = 2;
    private float _distanceTravelled;
    
    void Start()
    {
        GetComponent<MeshFilter>().mesh = Meshes[Random.Range(0, Meshes.Count)];
    }

    void Update()
    {
        _distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled);
        transform.eulerAngles -= new Vector3(90, 0, 0);
    }
}
