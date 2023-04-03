using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private Text score;
    private Fishing _fishingScript;

    void Start()
    {
        _fishingScript = FindObjectOfType<Fishing>();
    }

    void Update()
    {
        score.text = _fishingScript.score.ToString();
    }
}
