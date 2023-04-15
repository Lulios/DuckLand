using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int modeGame;
    public static bool ChangeScene = false;

    [SerializeField] private VRCameraFade vRCameraFade;
    [SerializeField] private GameObject child;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickDuckFishing()
    {
        modeGame = 1;
        StartCoroutine(LoadScene());
    }

    public void OnClickWhackADuck()
    {
        modeGame = 0;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        vRCameraFade.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FishingGame");
        child.SetActive(false);
        ChangeScene = true;
    }
}
