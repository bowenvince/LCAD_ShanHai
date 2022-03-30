using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HintButton : MonoBehaviour
{
    private Vector3 original_pos;
    [SerializeField]
    private Transform target_pos;

    private bool isActive;
    private Tween active_tween;

    private void Awake()
    {
        original_pos = transform.position;
        isActive = false;
    }

    public void Move()
    {
        if (isActive)
        {
            if (active_tween != null)
                active_tween.Kill();
            active_tween = transform.DOMove(original_pos, 1f);
            isActive = false;
        }
        else 
        {
            if (active_tween != null)
                active_tween.Kill();
            active_tween = transform.DOMove(target_pos.position, 0.5f);
            isActive = true;
        }
    }
}
