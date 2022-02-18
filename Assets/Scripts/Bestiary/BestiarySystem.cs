using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiarySystem : Singleton<BestiarySystem>
{
    [System.Serializable]
    public class BestiaryElement 
    {
        //obj: the object in Canvas need to be Set Active 
        public GameObject obj;

        //state:    0 = not collected
        //          1 = new
        //          2 = collected
        public int state = 0;
    }

    [System.Serializable]
    public class BestiarySet
    {
        public string bestiary_description;
        [SerializeField]
        public List<BestiaryElement> bestiaries = new List<BestiaryElement>();
    }

    [SerializeField]
    public List<BestiarySet> bestiaries = new List<BestiarySet>();


    //update state in record, change obj accordingly
    public void UpdateBestiary(int set_index, int index, int state) 
    {
        BestiaryElement element = bestiaries[set_index].bestiaries[index];
        element.obj.SetActive(state!=0);
        element.state = state;
    }

    //update all object with state in record
    public void UpdateBestiaryAll()
    {
        foreach (BestiarySet set in bestiaries) 
        {
            foreach (BestiaryElement element in set.bestiaries) 
            {
                element.obj.SetActive(element.state != 0);
            }
        }
    }

    //load data from file string
    public void LoadBestiaryData(string data) 
    {
/*        if (data.Length != bestiaries.Count) 
        {
            //if data not match, report error
        }
        for (int i = 0; i < data.Length; i++) 
        {
            bestiaries[i].state = (int)char.GetNumericValue(data[i]);
        }*/

    }
}
