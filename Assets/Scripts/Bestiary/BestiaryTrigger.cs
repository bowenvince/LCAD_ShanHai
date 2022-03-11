using UnityEngine;
using UnityEngine.Events;

public class BestiaryTrigger : MonoBehaviour, ITrigger
{
    public int bestiary_set_index;
    public int bestiary_element_index;
    public UnityEvent events;

    public Sprite item_image;
    public string item_name;

    public void AddBestiaryLine() 
    {
        BestiarySystem._this.UpdateBestiary(bestiary_set_index, bestiary_element_index, 1);
    }

    public void ShowNotification() 
    {
        ItemNotificationScript._this.show(item_image, item_name);
    }

    public void OnCall()
    {
        events.Invoke();
    }

    public void OnDestory()
    {
        Destroy(this.gameObject);
    }
}
