using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverNodeSet : MonoBehaviour
{
    [SerializeField]
    private List<HoverNode> node_list;
    public bool is_failed = false;

    [SerializeField]
    private int current_index;
    [SerializeField]
    private HoverNode current_node;

    private bool is_complete;
    public bool Check_status => is_complete; 

    private void Start()
    {
        current_index = 0;
        if (node_list.Count > 0) 
        {
            current_node = node_list[current_index];
            current_node.is_active = true;
        }
    }

    public void On_Node_Completed(HoverNode node) 
    {
        //check if node match current node
        if (node == current_node)
        {
            current_index++;

            //check if it reaches the end
            if (current_index >= node_list.Count)
            {
                //succeed
                is_complete = true;
                foreach (HoverNode node1 in node_list)
                {
                    node1.is_active = false;
                }
                Debug.Log("Complete.");
                this.gameObject.SetActive(false);
            }
            //if not end, move to next node
            else
            {
                //update current node
                current_node = node_list[current_index];
                current_node.is_active = true;
            }
        }
        else 
        {
            //reset all nodes status
            foreach (HoverNode node1 in node_list)
            {
                node1.Reset();
            }

            //reset index and current node if failed
            current_index = 0;
            current_node = null;
            if (node_list.Count > 0)
            {
                current_node = node_list[current_index];
                current_node.is_active = true;
            }

        }
    }

    private void Update()
    {
        if (is_failed) 
        {
            Debug.Log("Failed.");
            this.gameObject.SetActive(false);
        }
    }
}
