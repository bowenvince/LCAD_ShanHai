using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ItemNotificationScript : Singleton<ItemNotificationScript>
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string text_begin;
    [SerializeField]
    private Image image;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        reset();
    }

    public void reset()
    {
        canvasGroup.alpha = 0;
    }

    public void show(Sprite sprite, string name) 
    {
        reset();
        canvasGroup.DOComplete();
        canvasGroup.DOFade(1, 0.5f);
        text.text = text_begin + name;
        image.sprite = sprite;
        Sequence tween = DOTween.Sequence();
        tween.AppendInterval(2f);
        tween.Append(canvasGroup.DOFade(0, 1f));
    }
}
