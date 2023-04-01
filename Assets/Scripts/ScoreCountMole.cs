using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCountMole : MonoBehaviour
{
    [SerializeField] private Text Score;
    private WhackPool WhackPoolScript;

    void Start()
    {
        WhackPoolScript = FindObjectOfType<WhackPool>();
    }

    void Update()
    {
        Score.text = WhackPoolScript.Score.ToString();
    }
}
