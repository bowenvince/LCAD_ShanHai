using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailsmanSystem : MonoBehaviour
{
    #region Helper Class
    [System.Serializable]
    public class Orb
    {
        public string name;
        public GameObject obj;
    }
    #endregion

    private int current_index;

    [SerializeField]
    private List<Orb> orb_obj;

    //switch orb from current to new index
    public void SwitchOrb(int index)
    {
        orb_obj[current_index].obj.SetActive(false);
        current_index = index;
        orb_obj[current_index].obj.SetActive(true);
    }

    //check if current element match
    public bool CheckElement(string name) 
    {
        return (name == orb_obj[current_index].name);
    }
}
