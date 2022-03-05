﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float maxTime = 1f;

    private bool is_active = false;
    private Vector3 startPos;
    private float startTime;
    private Vector3 endPos;
    private float endTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !is_active)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            startPos = position;
            startTime = Time.time;

            is_active = true;
        }

        if (Input.GetMouseButtonUp(0) && is_active)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            endPos = position;
            endTime = Time.time;

            DetectSwipe();
            is_active = false;
        }
    }

    private void DetectSwipe() 
    {
        if (Vector3.Distance(endPos, startPos) >= minimumDistance &&
            (endTime - startTime) <= maxTime) 
        {
            //filp page
            if (endPos.x < startPos.x)
            {
                //Debug.Log("Swipe Left");
                BestiarySystem._this.FilpPage(1);
            }
            else 
            {
                //Debug.Log("Swipe Right");
                BestiarySystem._this.FilpPage(-1);
            }
            
            
        }
    }
}
