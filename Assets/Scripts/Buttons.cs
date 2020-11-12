using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    public GameObject[] names = new GameObject[4];
    [SerializeField] GameObject mainScene = null;
    [SerializeField] GameObject levelSelector = null;
    [SerializeField] GameObject settingsScreen = null;

    private void Awake()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].SetActive(false);
        }
    }
    public void LevelSelector(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void Back()
    {
        mainScene.SetActive(true);
        levelSelector.SetActive(false);
    }

    public void Play()
    {
        mainScene.SetActive(false);
        levelSelector.SetActive(true);
    }
    public void Options()
    {
        settingsScreen.SetActive(true);
        mainScene.SetActive(false);
    }
    public void Credits()
    {
        if (names[0].activeSelf)
        {
            for (int i = 0; i < names.Length; i++)
            {
                names[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < names.Length; i++)
            {
                names[i].SetActive(true);
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
