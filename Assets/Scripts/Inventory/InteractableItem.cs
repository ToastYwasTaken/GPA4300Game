using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: InteractableItem.cs
 * Version: 1.0
 * Autor: René Kraus (RK); Franz Mörike (FM); Jan Pagel (JP)
 * 
 * 
 * These coded instructions, statements, and computer programs contain
 * proprietary information of the author and are protected by Federal
 * copyright law. They may not be disclosed to third parties or copied
 * or duplicated in any form, in whole or in part, without the prior
 * written consent of the author.
 * 
 * ChangeLog
 * ----------------------------
 *  28.07.2021  FM  Created
 *  30.07.2021  FM  Added handling Items on Collision
 *  
 *****************************************************************************/


/// <summary>
/// Adds a collider to the Item to make it Interactable
/// Add this script on all Pickable items
/// </summary>
public class InteractableItem : MonoBehaviour
{
    public float colliderLength = 1f;
    private Vector3 colliderSize;
    public InventoryGUI inventoryGUIRef;
    private bool triggerFlag = false;

    //Adds the collider
    private void Start()
    {
        //Initializing Collider for each Item
        colliderSize = new Vector3(colliderLength, colliderLength, colliderLength);
        if (this.gameObject.GetComponent<Collider>() == null)
        {
            this.gameObject.AddComponent<BoxCollider>();
            GetComponent<BoxCollider>().size = colliderSize;
        }
        BoxCollider currentCollider = this.gameObject.GetComponent<BoxCollider>();
        colliderSize = currentCollider.size;
        currentCollider.isTrigger = true;
    }
    private void Update()
    {
        if(triggerFlag == true)
        {
            Destroy(gameObject);
        }
    }
    //Draws a box equal to the size of the future collider
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, colliderSize);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Picks up item on left mouse press
            if (Input.GetKeyDown(KeyCode.Mouse0) && this.tag.Equals("Item"))
            {
                //Add item to inventory / show in GUI
                Debug.Log("In trigger");
                Item item = this.gameObject.GetComponent<Item>();
                if (item)
                {
                    inventoryGUIRef.AddItem(item);
                    triggerFlag = true;
                }
            }
        }
    }

}
