﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<GenericAnt> ants = new List<GenericAnt>();
    private Camera _cam;
    public Vector3 offset;
    public float smoothTime=5f;
    public float minZoom=40;
    public float maxZoom=10;
    public float zoomLimiter;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        offset = transform.position;
        ants.AddRange(GameObject.FindObjectsOfType<GenericAnt>());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ants.Count == 0)
            return;
        Move();
        Zoom();
    }
    Vector3 GetCenterPoint()
    {
        if (ants.Count == 1)
        {
            return ants[0].transform.position;
        }
        var bounds = new Bounds(ants[0].transform.position, Vector3.zero);
        for (int i = 0; i < ants.Count; i++)
        {
            bounds.Encapsulate(ants[i].transform.position);
        }
        return bounds.center;
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

    }
    float GreatestDistance()
    {
        var bounds = new Bounds(ants[0].transform.position, Vector3.zero);
        for (int i = 0; i < ants.Count; i++)
        {
            bounds.Encapsulate(ants[i].transform.position);
        }
        return bounds.size.x;
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GreatestDistance() / zoomLimiter);
        _cam.fieldOfView =Mathf.Lerp(_cam.fieldOfView,newZoom,Time.deltaTime);
    }
}
