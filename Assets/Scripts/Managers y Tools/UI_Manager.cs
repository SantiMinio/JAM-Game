using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] Text collectablesText = null;
    [SerializeField] GameObject loseScreen = null;
    [SerializeField] GameObject winScreen = null;
    [SerializeField] GameObject pauseUI = null;
    [SerializeField] GameObject[] stars = new GameObject[0];
    [SerializeField] GameObject settingsUI = null;

    public void RefreshCollectabeAmmount(int ammount, int required)
    {
        collectablesText.text = ammount + "/" + required;
    }

    public void RefreshScore(int score)
    {
        for (int i = 0; i < score; i++)
            stars[i].SetActive(true);
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
    }

    public void LoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void PauseScreen(bool on)
    {
        pauseUI.SetActive(on);
    }

    public void OptionsScreen()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        MusicManager.instance.DestroyThis();
        SceneManager.LoadScene(0);
    }
}
