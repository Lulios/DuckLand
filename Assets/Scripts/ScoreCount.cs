using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private Text Score;
    private fishing fishingScript;

    void Start()
    {
        fishingScript = FindObjectOfType<fishing>();
    }

    void Update()
    {
        Score.text = fishingScript.Score.ToString();
    }
}
