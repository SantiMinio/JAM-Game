using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetero : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel(int NextlvlIndex)
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(next >= SceneManager.sceneCountInBuildSettings ? 0 : next);
    }
}
