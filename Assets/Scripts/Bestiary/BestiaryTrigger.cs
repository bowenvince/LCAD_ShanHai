using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BestiaryTrigger : MonoBehaviour, ITrigger
{
    #region Helper Class
    [System.Serializable]
    public class BestiaryLine
    {
        public int bestiary_set_index;
        public int bestiary_element_index;
        public int bestiary_state = 1;
    }
    #endregion

    public List<BestiaryLine> bestiary_lines;
    public UnityEvent events;

    public Sprite item_image;
    public string item_name;

    public void AddBestiaryLines() 
    {
        foreach(BestiaryLine line in bestiary_lines)
            BestiarySystem._this.UpdateBestiary(line.bestiary_set_index, line.bestiary_element_index, line.bestiary_state);
    }

    public void ShowNotification() 
    {
        ItemNotificationScript._this.show(item_image, item_name);
    }

    public void OnCall()
    {
        if(events != null)
            events.Invoke();
    }

    public void OnDestory()
    {
        Destroy(this.gameObject);
    }
}
