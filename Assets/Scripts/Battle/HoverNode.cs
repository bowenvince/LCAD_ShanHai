using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoverNode : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    public Color m_MouseOverColor = Color.red;

    //This stores the GameObject’s original color
    public Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    SpriteRenderer m_Renderer;

    Tween tween_current;

    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<SpriteRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.color;
    }

    void OnMouseOver()
    {
        if (tween_current != null)
        {
            tween_current.Complete();
        }
        // Change the color of the GameObject to red when the mouse is over GameObject
        m_Renderer.color = m_MouseOverColor;

    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        tween_current = m_Renderer.DOColor(m_OriginalColor, 2f);

    }
}
