using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum gameMode { Fishing, WhackADuck };
    public gameMode modeGame;
    public static int difficulty = 0;

    [SerializeField] private GameObject fishingRod;
    [SerializeField] private GameObject pool;
    [SerializeField] private GameObject whackPool;
    [SerializeField] private VRCameraFade vRCameraFade;
    [SerializeField] private Text miniGameName;
    [SerializeField] private Text difficultyName;

    int index = 0;

    void Start()
    {
        modeGame = (gameMode)MainMenu.modeGame;
        ChangeGameMode();
    }

    public void ChangeGameMode()
    {
        switch(modeGame)
        {
            case gameMode.Fishing:
                StartCoroutine(LoadWhackADuck());
                break;
            case gameMode.WhackADuck:
                StartCoroutine(LoadFishing());
                break;
        }
    }

    private IEnumerator LoadWhackADuck()
    {
        if (MainMenu.ChangeScene)
        {
            MainMenu.ChangeScene = false;
        }
        else
        {
            vRCameraFade.FadeOut();
            yield return new WaitForSeconds(1);
        }
        vRCameraFade.FadeIn();
        modeGame = gameMode.WhackADuck;
        fishingRod.SetActive(true);
        fishingRod.SetActive(false);
        pool.SetActive(false);
        whackPool.SetActive(true);
        whackPool.GetComponent<WhackPool>().score = 0;
        miniGameName.text = "Fishing";
    }

    private IEnumerator LoadFishing()
    {
        if (MainMenu.ChangeScene)
        {
            MainMenu.ChangeScene = false;
        }
        else
        {
            vRCameraFade.FadeOut();
            yield return new WaitForSeconds(1);
        }
        vRCameraFade.FadeIn();
        modeGame = gameMode.Fishing;
        fishingRod.SetActive(false);
        fishingRod.SetActive(true);
        pool.SetActive(true);
        fishingRod.GetComponent<Fishing>().score = 0;
        whackPool.SetActive(false);
        miniGameName.text = "Whack-A-Duck";
    }

    public void changeDifficulty()
    {
        if (GameManager.difficulty == 0)
        {
            GameManager.difficulty = 1;
            modeGame = (gameMode)(((int)modeGame + 1) % 2);
            ChangeGameMode();
            difficultyName.text = "Switch to easy";
        } 
        else if (GameManager.difficulty == 1)
        {
            GameManager.difficulty = 0;
            modeGame = (gameMode)(((int)modeGame + 1) % 2);
            ChangeGameMode();
            difficultyName.text = "Switch to hard";
        }
    }
}
