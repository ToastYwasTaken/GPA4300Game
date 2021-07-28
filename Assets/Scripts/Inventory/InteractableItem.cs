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


    //Adds the collider
    private void Start()
    {
        colliderSize = new Vector3(colliderLength, colliderLength, colliderLength);
        if (this.gameObject.GetComponent<Collider>() == null)
        {
            this.gameObject.AddComponent<BoxCollider>();
            GetComponent<BoxCollider>().size = colliderSize;
        }
        BoxCollider currentCollider = this.gameObject.GetComponent<BoxCollider>();
        colliderSize = currentCollider.size;
    }

    //Draws a box equal to the size of the future collider
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, colliderSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Picks up item on left mouse press
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Add item to inventory
            }
        }
    }
}
