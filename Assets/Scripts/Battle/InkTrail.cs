using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkTrail : MonoBehaviour
{
    private bool is_active = false;
    public GameObject inkTrailPrefab;

    public Camera camera_current;

    public GameObject inkTrailParent;
    public GameObject inkTrailCurrent;

    private void Update()
    {
        if (is_active && inkTrailCurrent)
        {
            Vector3 position = camera_current.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            inkTrailCurrent.transform.position = position;
        }

        if (Input.GetMouseButtonDown(0) && !is_active) 
        {
            inkTrailCurrent = Instantiate<GameObject>(inkTrailPrefab);
            inkTrailCurrent.transform.SetParent(inkTrailParent.transform);

            Vector3 position = camera_current.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            inkTrailCurrent.transform.position = position;

            is_active = true;
        }

        if (Input.GetMouseButtonUp(0) && is_active)
        {
            is_active = false;
        }
    }
}
