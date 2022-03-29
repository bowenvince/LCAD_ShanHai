using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PasscodePanel : MonoBehaviour
{
    public string Key;
    public bool Case_Sensitive = false;
    public List<PasscodeSlot> slots;
    public UnityEvent on_match;


    public void Check_Input() 
    {
        string temp_input = "";
        foreach (PasscodeSlot slot in slots) 
        {
            temp_input += slot.current_text;
        }
        if (!Case_Sensitive) 
        {
            Key = Key.ToLower();
            temp_input = temp_input.ToLower();
        }
        if (Key == temp_input) 
        {
            on_match.Invoke();
            Debug.Log("Key Matched.");
        }
    }
}
