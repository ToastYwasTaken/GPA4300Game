using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: Item.cs
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
 *  09.07.2021  FM  erstellt
 *  30.07.2021  FM  von ScriptableObject zu MonoBehaviour erbende Klasse verändert
 *                  für bessere Funktionalität
 *  01.08.2021  FM  Properties hinzugefügt, Zugriffsmodifizierer angepasst, 
 *                  automatisches Initialisieren für GO und Name hinzugefügt
 *  14.08.2021  FM  Kommentare hinzugefügt
 *  21.08.2021  FM  Use() hinzugefügt, GetItemType() hjinzugefügt
 *  
 *****************************************************************************/

/// <summary>
/// Items
/// Sprites sollten im Inspektor gesetzt werden
/// </summary>
public class Item : MonoBehaviour
    {
    public string itemName;
    [SerializeField]
    private Sprite sprite;  //Das Sprite, das in der GUI angezeigt werden soll
    [SerializeField]
    private IItemTypes.ItemType itemType;

    [SerializeField]
    private Animator gateAnimator;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GUIInventory inventoryRef;

    private sbyte healValue = 20;

    private void Start()
    {
        SetDefaultItemName();
        SetDefaultItemtype();
    }


    public Item()
    {

    }

    /// <summary>
    /// Setzt standartisierte Werte für den Item-Namen, falls es nicht im 
    /// Inspektor initialisiert wurde
    /// </summary>
    private void SetDefaultItemName()
    {
        if (this.itemName == null)
        {
            itemName = this.gameObject.name;
        }
    }
    private void SetDefaultItemtype()
    {
        this.itemType = GetItemType();
    }
    private IItemTypes.ItemType GetItemType()
    {
        switch (itemName)
        {
            case "key" :
                return IItemTypes.ItemType.Key;
            case "healPotion" :
                return IItemTypes.ItemType.HealPotion;
            case "sprintPotion":
                return IItemTypes.ItemType.SprintPotion;
            case "map1":
                return IItemTypes.ItemType.MapPart1;
            case "map2":
                return IItemTypes.ItemType.MapPart2;
            case "map3":
                return IItemTypes.ItemType.MapPart3;
            default:
                return IItemTypes.ItemType.None;
        }
    }

    /// <summary>
    /// Sorgt dafür, dass der Spieler seine gesammelten Items
    /// anwenden kann
    /// Wenn der Spieler das Item nicht einsetzen kann erscheint ein
    /// Hinweis...
    /// </summary>
    public void Use()
    {
        Debug.Log("Using Item: " + itemType.ToString());
        bool itemUsed = false;
        int indexOfMapInInventory;
        //Kollidiert mit ExitGate 
        if (playerController.playerCollidingWithExitGate)
        {
            //wenn key -> öffnen, sonst->anzeigen key fehlt
            if (itemType.Equals(IItemTypes.ItemType.Key))
            {
                Debug.Log("Colliding with exit gate");
                //Gate Animation
                gateAnimator.SetTrigger("Switch");
                itemUsed = true;
            }
            if (itemUsed)
            {
                inventoryRef.RemoveItem(this);
            }
        }
        //Kollidiert nicht mit ExitGate
        else
        {
            //alle items können verwendet werden außer key
            switch (itemType)
            {
                case IItemTypes.ItemType.Key:
                    //Zeige an, dass der Spieler nicht beim Gate ist
                    inventoryRef.StartCoroutine(inventoryRef.DisplayNotAtGate());
                    break;
                case IItemTypes.ItemType.HealPotion:
                    //Zeige an, dass der Spieler volle Leben hat und deshalb
                    //den Trank nicht benutzen kann
                    if (playerController.HealthProperty >= 100)
                    {
                        inventoryRef.StartCoroutine(inventoryRef.DisplayCantUseHealpotion());
                    }
                    else
                    {
                        playerController.HealthProperty += healValue;
                        itemUsed = true;
                    }
                    break;
                case IItemTypes.ItemType.SprintPotion:
                    //TODO: Use sprint potion
                    if(playerController.Endurance >= playerController.maxEndurance)
                    {
                        inventoryRef.StartCoroutine(inventoryRef.DisplayCantUseEndurancepotion());
                    }
                    else
                    {

                        itemUsed = true;
                    }
                    break;
                case IItemTypes.ItemType.MapPart1:
                    indexOfMapInInventory = inventoryRef.PGetInventory
                        .IndexOf(inventoryRef.PGetInventory
                        .Find(x => x.itemType == IItemTypes.ItemType.MapPart1));
                    inventoryRef.OpenMap(1, indexOfMapInInventory);
                    break;
                case IItemTypes.ItemType.MapPart2:
                    indexOfMapInInventory = inventoryRef.PGetInventory
                        .IndexOf(inventoryRef.PGetInventory
                        .Find(x => x.itemType == IItemTypes.ItemType.MapPart2));
                    inventoryRef.OpenMap(2, indexOfMapInInventory);
                    break;
                case IItemTypes.ItemType.MapPart3:
                    indexOfMapInInventory = inventoryRef.PGetInventory
                        .IndexOf(inventoryRef.PGetInventory
                        .Find(x => x.itemType == IItemTypes.ItemType.MapPart3));
                    inventoryRef.OpenMap(3, indexOfMapInInventory);
                    break;
                case IItemTypes.ItemType.None:
                    break;
                default:
                    break;
            }
            if (itemUsed)
            {
                inventoryRef.RemoveItem(this);
            }
        }
    }


    public IItemTypes.ItemType PItemType { get => itemType; set => itemType = value; }
    public Sprite PItemSprite { get => sprite; }
}
