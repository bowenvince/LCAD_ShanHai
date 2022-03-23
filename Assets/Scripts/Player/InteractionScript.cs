using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class InteractionScript : MonoBehaviour
{
    public bool CanInteract = false;
    public GameObject Interact_Guide_canvas;
    public GameObject DialogChat_canvas;

    public GameObject DialogBox_canvas;
    public GameObject Avatar_left;
    public GameObject Avatar_right;

    private Collider2D current_target;

    [SerializeField]
    private DialogSystem current_dialogSys;

    public float Auto_Dialog_Delay = 0f;
    public bool Auto_Dialog = true;

    //for DialogBox
    private bool DialogBox_started = false;
    private DialogNode current_node;

    //for touch screen adaption
    [SerializeField]
    private GameObject Interact_Button_obj;
    private Button Interact_Button;


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
                StartCoroutine(WaitAndShowDialogChat(Auto_Dialog_Delay));
            }

            //touch screen adaption - show & bind button with interaction
            Interact_Button.onClick.RemoveAllListeners();
            Interact_Button.onClick.AddListener(()=>ShowDialogBox());
            Interact_Button_obj.SetActive(true);
        }
        else if (other.tag == "Item")
        {
            CanInteract = true;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().alpha = 0;
            Interact_Guide_canvas.GetComponent<CanvasGroup>().DOComplete();
            Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            current_target = other;

            if (other.TryGetComponent(out ITrigger get_trigger))
            {
                Interact_Button.onClick.RemoveAllListeners();
                Interact_Button.onClick.AddListener(() => get_trigger.OnCall());
                Interact_Button_obj.SetActive(true);
            }

            //touch screen adaption - show & bind button with interaction
            
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
            HideDialogChat();
            current_dialogSys = null;
        }

        //touch screen adaption - hide button with interaction
        Interact_Button_obj.SetActive(false);
        Interact_Button.onClick.RemoveAllListeners();

    }

    private void Update()
    {
        //fix interact guide ui rotation
        if (Mathf.Sign(gameObject.transform.localScale.x) != Mathf.Sign(Interact_Guide_canvas.transform.localScale.x))
        {
            Interact_Guide_canvas.transform.localScale = new Vector3(Interact_Guide_canvas.transform.localScale.x * -1f, Interact_Guide_canvas.transform.localScale.y, Interact_Guide_canvas.transform.localScale.z);
        }
        /*if (Input.GetKey(KeyCode.E) && CanInteract && current_dialogSys && !DialogBox_started) 
        {
            ShowDialogBox();
        }*/
    }

    private void Awake()
    {
        //interact guide setup
        Interact_Guide_canvas.GetComponent<CanvasGroup>().alpha = 0;
        //dialog canvas setup
        DialogChat_canvas.SetActive(false);
        DialogChat_canvas.GetComponent<CanvasGroup>().alpha = 0;
        DialogBox_canvas.SetActive(false);
        DialogBox_canvas.GetComponent<CanvasGroup>().alpha = 0;

        //touch screen adaption - get button from obj
        Interact_Button = Interact_Button_obj.GetComponent<Button>();
    }

    //auto dialog for chat
    public void ShowDialogChat() 
    {
        if (Auto_Dialog && CanInteract)
        {
            Debug.Log("Talking to ; ");
            //show dialog canvas + fade-in
            if (current_target)
            {
                DialogChat_canvas.SetActive(true);
                DialogChat_canvas.transform.position = current_target.transform.position + new Vector3(0, 1f, 0);
                DialogChat_canvas.GetComponent<CanvasGroup>().alpha = 0;
                DialogChat_canvas.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
                //interact guide fade-out
                Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
                //update text if possible
                if (current_dialogSys)
                {
                    if (current_dialogSys.Get_Current_Dialog_Chat() != null)
                    {
                        DialogChatSO current_dialog = current_dialogSys.Get_Current_Dialog_Chat();
                        DialogChat_canvas.GetComponentInChildren<TextMeshProUGUI>().text = current_dialog.text;
                        current_dialog.Process_Solution();

                    }
                    else
                    {
                        DialogChat_canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Holder";
                    }
                }
                else
                {
                    DialogChat_canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Holder";
                }
            }
        }
    }

    //button triggered dialog box
    public void ShowDialogBox()
    {
        if (CanInteract)
        {
            Debug.Log("Talking to ; ");
            if (current_target)
            {
                if (current_dialogSys)
                {
                    DialogBoxSO current_dialog = current_dialogSys.Get_Current_Dialog_Box();
                    Debug.Log("current_dialog = " + current_dialog);

                    if (current_dialog != null)
                    {
                        //interact guide fade-out
                        Interact_Guide_canvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
                        //close any chat if open
                        if (DialogChat_canvas.activeSelf) HideDialogChat();

                        DialogBox_canvas.GetComponent<CanvasGroup>().alpha = 0;
                        DialogBox_canvas.SetActive(true);
                        DialogBox_canvas.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

                        DialogBox_started = true;
                        //disable playermovement
                        GetComponent<PlayerMovement>().enabled = false;
                        current_node = current_dialog.first_node;
                        if (!current_node) return;
                        TextTyper._this.Reset();
                        UpdateDialogBox();
                    }
                    else
                    {
                        //if no current_dialog (no condition matched), nothing happen
                    }
                }
                else
                {
                    //if no current_dialogSys (DialogSystem is not attached), nothing happen
                }
            }
        }
    }

    public void UpdateDialogBox() 
    {
        //check typing animation end?
        if (TextTyper._this.is_typing)
        {
            TextTyper._this.SkipEffect();
        }
        else 
        {
            //if no current node => reached end of dialog
            if (!current_node)
            {
                //call end of dialog
                EndDialogBox();
                return;
            }
            //update sprite
            if (current_node) 
            {
                if (current_node.NarrationLine_current.Character.is_player)
                {
                    //simple show the current and hide the other avatar
                    Avatar_right.SetActive(true);
                    Avatar_left.SetActive(false);
                    Avatar_right.GetComponent<Image>().sprite = current_node.NarrationLine_current.Character.Sprite;
                }
                else 
                {
                    //simple show the current and hide the other avatar
                    Avatar_right.SetActive(false);
                    Avatar_left.SetActive(true);
                    Avatar_left.GetComponent<Image>().sprite = current_node.NarrationLine_current.Character.Sprite;
                }
            }
            //update name

            string named_text = current_node.NarrationLine_current.Character.Name +": "+ current_node.NarrationLine_current.Text;
            TextTyper._this.DisplayText_EffectTypeWriter(named_text);
            if (current_node.GetType() == typeof(DialogBasicNode))
            {
                //go to next node
                current_node = current_node.Next_node();
            }
            else if (current_node.GetType() == typeof(DialogChoiceNode)) 
            {
                //show options
            }
        }
    }

    //end of dialogBox, process solution and hide dialog box panel
    public void EndDialogBox() 
    {
        DialogBoxSO current_dialog = current_dialogSys.Get_Current_Dialog_Box();
        current_dialog.Process_Solution();
        DialogBox_started = false;
        HideDialogBox();
        //re-enable playermovement
        GetComponent<PlayerMovement>().enabled = true;
    }

    public void HideDialogChat() 
    {
        //disable dialog canvas
        if (DialogChat_canvas.activeSelf)
        {
            DialogChat_canvas.SetActive(false);
        }
    }

    public void HideDialogBox()
    {
        //disable dialog canvas
        if (DialogBox_canvas.activeSelf)
        {
            DialogBox_canvas.SetActive(false);
        }
    }

    private IEnumerator WaitAndShowDialogChat(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (CanInteract) 
        {
            ShowDialogChat();
        }
        
    }
}
