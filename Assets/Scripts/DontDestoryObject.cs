using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    public string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (DontDestoryObject obj in Object.FindObjectsOfType<DontDestoryObject>()) 
        {
            if (obj != this && obj.objectID == objectID)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
