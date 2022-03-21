using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FishingBar : MonoBehaviour
{
    //Gradually move right
    //Click move left

    public float right_amount = 1;
    public float left_amount = -2;
    public float slider_maximum = 500;
    public float accept_change_time = 5f;
    public float accept_change_range_min = 50;
    public float accept_change_range_max = 150;

    public Slider succeedBar;
    public float succeed_amount_max = 100;
    public float succeed_amount = 0.05f;

    //replace succeed bar with circular version
    public Image succeedFill;

    public Slider failBar;
    public float fail_amount_max = 100;
    public float fail_amount = 0.05f;

    public RectTransform current_accept_area_left;
    public RectTransform current_accept_area_right;
    public RectTransform current_accept_area;

    private Slider slider;
    private float current_accept_value = 225; //minimum
    private float current_accept_range = 50; //+value=maximum
    private float current_accept_change_timer;

    public bool is_active;

    public GameObject result_panel;
    public GameObject succeed_text;
    public GameObject failed_text;

    //update questLine
    public bool update_questPath = false;
    public QuestPathSO result_questPath;
    public int result_questPath_from;
    public int result_questPath_to;

    public bool button_is_press = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider) 
        {
            slider.maxValue = slider_maximum;
        }
        current_accept_change_timer = accept_change_time;
        if (succeedBar) 
        {
            succeedBar.maxValue = succeed_amount_max;
        }
        if (failBar)
        {
            failBar.maxValue = fail_amount_max;
        }
        Change_Accept_Area();
    }

    // Update is called once per frame
    void Update()
    {
        //check if system is enable
        if (!is_active) return;
        //check if paused
        if (Time.timeScale == 0) return;
        //if button pushed, add left amount, else right
        //if (Input.GetMouseButton(0))
        if(button_is_press)
        {
            slider.value += left_amount;
        }
        else 
        {
            slider.value += right_amount;
        }

        //check if current frame is vaild
        if (slider.value >= current_accept_value && slider.value <= current_accept_value + current_accept_range)
        {
            succeedBar.value += succeed_amount;
        }
        else 
        {
            failBar.value += fail_amount;
        }
        Check_Condition();

        //count in timer
        current_accept_change_timer -= Time.deltaTime;
        if (current_accept_change_timer <= 0f) 
        {
            current_accept_change_timer = accept_change_time;
            Change_Accept_Area();
        }

        //update succeed fill value
        succeedFill.fillAmount = succeedBar.value / succeedBar.maxValue;
    }

    void Change_Accept_Area() 
    {
        Debug.Log("change accept area");

        //randomize area
        current_accept_range = Random.Range(accept_change_range_min, accept_change_range_max);
        current_accept_value = Random.Range(0, slider_maximum - current_accept_range);
        Update_AcceptArea_Display();
    }

    void Update_AcceptArea_Display() 
    {
        if (current_accept_area_left != null) 
        {
            current_accept_area_left.anchorMin = new Vector2(current_accept_value / slider_maximum, 0f);
            current_accept_area_left.anchorMax = new Vector2(current_accept_value / slider_maximum, 1f);
            current_accept_area_left.anchoredPosition = new Vector2(0f, 0f);
        }
        if (current_accept_area_right != null) 
        {
            current_accept_area_right.anchorMin = new Vector2((current_accept_value + current_accept_range) / slider_maximum, 0f);
            current_accept_area_right.anchorMax = new Vector2((current_accept_value + current_accept_range) / slider_maximum, 1f);
            current_accept_area_right.anchoredPosition = new Vector2(0f, 0f);
        }
        if (current_accept_area != null) 
        {
            current_accept_area.DOAnchorMin(new Vector2(current_accept_value / slider_maximum, 0f), 1f);
            current_accept_area.DOAnchorMax(new Vector2((current_accept_value + current_accept_range) / slider_maximum, 1f), 1f);
            current_accept_area.DOAnchorPos(new Vector2(0f, 0f), 1f);
        }

    }

    void Check_Condition() 
    {
        if (succeedBar.value >= succeed_amount_max)
        {
            is_active = false;
            succeed_text.SetActive(true);
            result_panel.SetActive(true);

            //update quest
            QuestSystem._this.Update_State(result_questPath, result_questPath_from, result_questPath_to);
        }
        else if(failBar.value >= fail_amount_max)
        {
            is_active = false;
            failed_text.SetActive(true);
            result_panel.SetActive(true);
        }
    }

    void Reset_Result() 
    {
        is_active = true;
        succeedBar.value = 0;
        failBar.value = 0;
        succeed_text.SetActive(false);
        failed_text.SetActive(false);
        result_panel.SetActive(false);
    }

    public void OnButtonPress() 
    {
        button_is_press = true;
    }

    public void OnButtonRelease()
    {
        button_is_press = false;
    }
}
