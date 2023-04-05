using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishingRod;
    [SerializeField] private GameObject pool;
    [SerializeField] private GameObject whackPool;

    private enum gameMode { Fishing, WhackADuck };
    private gameMode modeGame;

    int index = 0;

    private void Start()
    {
        modeGame = gameMode.WhackADuck;
    }

    public void ChangeGameMode()
    {

        
        Debug.Log("Click - " + index);
        index++;
   

       /* switch(modeGame)
        {
            case gameMode.Fishing:
                modeGame = gameMode.WhackADuck;
                fishingRod.SetActive(false);
                pool.SetActive(false);
                whackPool.SetActive(true);
                break;
            case gameMode.WhackADuck:
                modeGame = gameMode.Fishing;
                fishingRod.SetActive(true);
                pool.SetActive(true);
                whackPool.SetActive(false);
                break;
        }*/
    }

}
