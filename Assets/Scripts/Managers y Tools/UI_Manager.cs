using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectablesText = null;
    [SerializeField] GameObject loseScreen = null;
    [SerializeField] GameObject winScreen = null;
    [SerializeField] GameObject pauseUI = null;
    [SerializeField] GameObject[] stars = new GameObject[0];
    [SerializeField] GameObject settingsUI = null;
    int starAmount;
    public void RefreshCollectabeAmmount(int ammount, int required)
    {
        collectablesText.text = ammount + "/" + required;
    }

    public void RefreshScore(int score)
    {
        starAmount=score;
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
        StartCoroutine(StartsDelay(starAmount));

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
    IEnumerator StartsDelay(int startsAmount)
    {
        int current = 0;
        while (current < startsAmount)
        {
            yield return new WaitForSeconds(0.5f);
            stars[current].SetActive(true);
            current++;
        }
    }
}
