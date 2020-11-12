using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject[] stars = new GameObject[0];
    [SerializeField] GameObject blockedVersion = null;
    [SerializeField] GameObject normalVersion = null;
    
    public void BlockLevel(bool block)
    {
        blockedVersion.SetActive(block);
        normalVersion.SetActive(!block);
    }

    public void RefreshStars(int _stars)
    {
        for (int i = 0; i < _stars; i++)
            stars[i].SetActive(true);

        for (int i = _stars; i < stars.Length; i++)
            stars[i].SetActive(false);
    }
}
