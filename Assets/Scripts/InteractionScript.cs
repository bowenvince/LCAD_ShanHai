using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InteractionScript : MonoBehaviour
{
    public bool CanInteract = false;
    public GameObject Interact_Guide_canvas;
    public GameObject Dialog_canvas;

    private Collider2D current_target;

    [SerializeField]
    private DialogSystem current_dialogSys;

    public float Auto_Dialog_Delay = 0f;
    public bool Auto_Dialog = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            CanInteract = true;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().alpha = 0;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().DOComplete();
            Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            current_target = other;
            if (other.TryGetComponent(out DialogSystem get_dialogSys)) 
            {
                current_dialogSys = get_dialogSys;
                StartCoroutine(WaitAndShowDialog(Auto_Dialog_Delay));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*if (other.tag == "NPC")
        {
            CanInteract = true;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().alpha = 0;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
        }*/
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        CanInteract = false;
        //interact guide fade-out
        Interact_Guide_canvas.GetComponent<CanvasGroup>().DOComplete();
        Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
        if (other.tag == "NPC") 
        {
            HideDialog();
        }
        
    }

    private void Update()
    {
        //fix interact guide ui rotation
        if (Mathf.Sign(gameObject.transform.localScale.x) != Mathf.Sign(Interact_Guide_canvas.transform.localScale.x))
        {
            Interact_Guide_canvas.transform.localScale = new Vector3(Interact_Guide_canvas.transform.localScale.x * -1f, Interact_Guide_canvas.transform.localScale.y, Interact_Guide_canvas.transform.localScale.z);
        }
    }

    private void Awake()
    {
        //interact guide setup
        Interact_Guide_canvas.GetComponent<CanvasGroup>().alpha = 0;
        //dialog canvas setup
        Dialog_canvas.SetActive(false);
        Dialog_canvas.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ShowDialog() 
    {
        if ((Input.GetKeyDown(KeyCode.E) || Auto_Dialog) && CanInteract)
        {
            Debug.Log("Talking to ; ");
            //show dialog canvas + fade-in
            if (current_target)
            {
                Dialog_canvas.SetActive(true);
                Dialog_canvas.transform.position = current_target.transform.position + new Vector3(0, 1f, 0);
                Dialog_canvas.GetComponent<CanvasGroup>().alpha = 0;
                Dialog_canvas.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
                //interact guide fade-out
                Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
                //update text if possible
                if (current_dialogSys)
                {
                    if (current_dialogSys.Get_Current_Dialog() != null)
                    {
                        DialogSO current_dialog = current_dialogSys.Get_Current_Dialog();
                        Dialog_canvas.GetComponentInChildren<TextMeshProUGUI>().text = current_dialog.text;
                        current_dialog.Process_Solution();

                    }
                    else
                    {
                        Dialog_canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Holder";
                    }
                }
                else
                {
                    Dialog_canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Holder";
                }
            }
        }
    }

    public void HideDialog() 
    {
        //disable dialog canvas
        if (Dialog_canvas.activeSelf)
        {
            Dialog_canvas.SetActive(false);
            current_dialogSys = null;
        }
    }

    private IEnumerator WaitAndShowDialog(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (CanInteract) 
        {
            ShowDialog();
        }
        
    }
}
