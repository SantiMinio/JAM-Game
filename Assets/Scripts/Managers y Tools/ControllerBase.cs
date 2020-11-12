﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour, IPauseable
{
    public Action<Vector3> MovementInput = delegate { };
    bool movementInCD;
    [SerializeField] float movementCooldown = 0.5f;
    float gridSpacing = 1;
    float timer;
    bool startCD;
    [HideInInspector] public bool playing = true;

    [HideInInspector] public List<Func<bool>> questionList = new List<Func<bool>>();

    bool isPaused;

    private void Start()
    {
        gridSpacing = GameManager.Instance.gridSpacing;
        //GameManager.Instance.StopMove += () => startCD = true;
        GameManager.Instance.pauseManager.AddToPause(this);
    }

    void Update()
    {
        if (!playing || isPaused) return; 

        if(startCD)
        {
            timer += Time.deltaTime;
            if (timer >= movementCooldown)
            {
                timer = 0;
                if (!Question())
                {
                    movementInCD = false;
                    startCD = false;
                }
            }
        }

        if (movementInCD) return;

        if (Input.GetAxis("Vertical") != 0)
        {
            MovementInput(new Vector3(0, 0, gridSpacing * ClampToInt(Input.GetAxis("Vertical"))));
            movementInCD = true;
            startCD = true;
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            MovementInput(new Vector3(gridSpacing * ClampToInt(Input.GetAxis("Horizontal")), 0, 0));
            movementInCD = true;
            startCD = true;
        }
    }

    float ClampToInt(float num)
    {
        float result = 0;
        if (num > 0) result = 1;
        else result = -1;

        return result;
    }

    bool Question()
    {
        for (int i = 0; i < questionList.Count; i++)
        {
            if (questionList[i]()) return true;
        }

        return false;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
