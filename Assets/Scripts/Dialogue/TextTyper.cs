using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public static TextTyper _this;

    [SerializeField]
    private float CharacterSpeed = 0.02f;
    [SerializeField]
    private bool InvisibleCharacter = true;
    [SerializeField]
    private GameObject DialogObj;

    [SerializeField]
    private bool m_is_typing = false;
    [SerializeField]
    private string m_current_text;
    private Coroutine m_typewriter;

    public bool is_typing => m_is_typing;

    public void DisplayText_EffectTypeWriter(string text)
    {
        DialogObj.SetActive(true);
        //StopAllCoroutines();
        m_typewriter = StartCoroutine(EffectTypeWriter(text, DialogObj.GetComponent<TextMeshProUGUI>()));
    }


    private IEnumerator EffectTypeWriter(string text, TextMeshProUGUI tmp)
    {
        m_is_typing = true;
        m_current_text = text;
        for (int i = 0; i < text.Length; i++)
        {
            tmp.text = text.Substring(0, i + 1);
            if (InvisibleCharacter)
            {
                tmp.text += "<color=#00000000>" + text.Substring(i + 1) + "</color>";
            }
            yield return new WaitForSeconds(CharacterSpeed);
        }
        m_is_typing = false;
    }

    public void SkipEffect() 
    {
        if(m_typewriter != null)
            StopAllCoroutines(); 
        DialogObj.GetComponent<TextMeshProUGUI>().text = m_current_text;
        m_is_typing = false;
        Debug.Log("Skipped Effect");
    }

    public void Reset()
    {
        m_is_typing = false;
        m_current_text = null;
        m_typewriter = null;
    }


    private void Awake()
    {
        _this = this;
    }

}
