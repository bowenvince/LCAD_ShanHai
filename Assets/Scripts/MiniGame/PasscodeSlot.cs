using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasscodeSlot : MonoBehaviour
{
    public List<string> option_codes;
    public Animator animator;

    private int current_code = 0;
    public string current_text { get { return option_codes[current_code]; } }

    public void NextOptions() 
    {
        if (++current_code >= option_codes.Count) 
        {
            current_code = 0;
        }

        animator.SetTrigger("Next");
    }

    private void OnEnable()
    {
        current_code = 0;
        animator.SetTrigger("Reset");
    }
}
