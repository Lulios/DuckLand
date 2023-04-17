using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 2 * GameManager.difficulty;
    private float _distanceTravelled;

    void Update()
    {
        _distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled);
        transform.eulerAngles -= new Vector3(90, 0, 0);
    }
}
