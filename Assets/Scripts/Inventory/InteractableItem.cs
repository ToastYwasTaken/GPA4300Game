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
 *  28.07.2021  FM  Erstellt
 *  30.07.2021  FM  Collision-detection hinzugefügt und Mechanik zum 
 *                  Items hinzufügen erstellt
 *  12.08.2021  FM  Bug behoben, der verursacht hat, dass der Spieler
 *                  ein Item aufheben konnte obwohl das Inventar voll war
 *  
 *****************************************************************************/


/// <summary>
/// Fügt dem Item einen eigenen Collider hinzu, damit es mit anderen 
/// Objekten interagieren kann
/// Dieses Skript muss auf alle Items
/// </summary>
public class InteractableItem : MonoBehaviour
{
    public float colliderLength = 1f;
    private Vector3 colliderSize;
    public GUIInventory inventoryGUIRef;
    private bool triggerFlag = false;

    
    private void Start()
    {
        //Iniialisieren des neuen Colliders
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
        //Zerstört das GO nur, wenn es vorher auch dem Inventar
        //hinzugefügt werden konnte
        if(triggerFlag == true)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        triggerFlag = false;
    }
    //Zeichnet den Collider
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, colliderSize);
    }

    
    private void OnTriggerStay(Collider other)
    {
        //Item nicht aufnehmbar, wenn das Inventar voll ist
        if (inventoryGUIRef.PGetInventory.Count >= inventoryGUIRef.inventoryMaxSize)
        {
            return;
        }
        if (other.tag.Equals("Player"))
        {
            //Picks up item on left mouse press
            if (Input.GetKeyDown(KeyCode.Mouse0) && this.tag.Equals("Item"))
            {
                //Füge Item ins Inventar hinzu / GUI Anzeige
                Debug.Log("In trigger");
                //TODO create new instance on Add
                Item item = this.gameObject.GetComponent<Item>();
                if (item && !triggerFlag)
                {
                    inventoryGUIRef.AddItem(item);
                    triggerFlag = true;
                }
            }
        }
    }

}
