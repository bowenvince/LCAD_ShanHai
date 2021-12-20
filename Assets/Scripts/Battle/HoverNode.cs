using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HoverNode : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (yellow)
    public Color m_MouseOverColor;

    //This stores the GameObject’s original color
    public Color m_OriginalColor;

    //When the timer ran out without mouse over, it turns to this color (red)
    public Color m_FailColor;

    //total time to catch other wise fade out
    public float fade_out_time;
    private float fade_out_time_left;

    //Get the GameObject’s Image Component (replaced mesh renderer) to access the GameObject’s material and color
    Image m_Image;

    //tempory store Tween obj
    Tween tween_current;


    //track status
    public bool is_complete;
    public bool is_active;



    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Image = GetComponent<Image>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Image.color;

        fade_out_time_left = fade_out_time;

        is_complete = false;

        //is_active = false;
    }

    void OnMouseOver()
    {
        if (!is_active)
        {
            m_Image.color = m_FailColor;
        }
        else 
        {
            if (tween_current != null)
            {
                tween_current.Complete();
            }
            // Change the color of the GameObject to red when the mouse is over GameObject
            m_Image.color = m_MouseOverColor;

            is_complete = true;

            m_Image.fillAmount = 1;
        }
        

    }

    void OnMouseExit()
    {
        if (!is_active) return;
        // Reset the color of the GameObject back to normal
        tween_current = m_Image.DOColor(m_OriginalColor, 2f);

    }

    private void Update()
    {
        if (fade_out_time_left > 0)
        {
            fade_out_time_left -= Time.deltaTime;
            if (!is_complete)
            {
                m_Image.fillAmount = Mathf.Clamp01(fade_out_time_left / fade_out_time);
            }
        }
        else 
        {
            if (!is_complete)
            {
                m_Image.fillAmount = 1;
                m_Image.color = m_FailColor;
            }
            
        }
        
    }
}
