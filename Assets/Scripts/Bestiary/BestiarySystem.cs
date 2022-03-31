using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BestiarySystem : Singleton<BestiarySystem>
{
    #region Helper Class
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
        public GameObject BestiaryPage_obj;
        [SerializeField]
        public List<BestiaryElement> bestiaries = new List<BestiaryElement>();
    }
    #endregion


    [SerializeField]
    public List<BestiarySet> bestiaries = new List<BestiarySet>();

    [SerializeField]
    private int current_page = 0;

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

    //Reset current page to index 0
    public void ResetPages() 
    {
        FilpPage(0);
    }

    //play animation and change to new page
    // return = how many pages filped 
    public int FilpPage(int direction) 
    {
        int newPageIndex = 0;
        if (direction > 0) 
        {
            newPageIndex = (current_page + 1 < bestiaries.Count) ? (current_page + 1) : 0;
        }
        else if (direction < 0)
        {
            newPageIndex = (current_page > 0) ? (current_page - 1) : bestiaries.Count - 1;
        }

        bestiaries[newPageIndex].BestiaryPage_obj.SetActive(true);
        bestiaries[current_page].BestiaryPage_obj.SetActive(false);
        //bestiaries[newPageIndex].BestiaryPage_obj.GetComponent<CanvasGroup>().alpha = 0f;
        //bestiaries[current_page].BestiaryPage_obj.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(()=> bestiaries[current_page].BestiaryPage_obj.SetActive(false));
        //bestiaries[newPageIndex].BestiaryPage_obj.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

        int page_filpped = Mathf.Abs(newPageIndex - current_page);
        current_page = newPageIndex;

        return page_filpped;
    }
}
