using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/******************************************************************************
 * Project: GPA4300Game
 * File: GUIInventory.cs
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
 *  30.07.2021  FM  Inventarstruktur überarbeitet
 *  01.08.2021  FM  Properties hinzugefügt, bugs behoben
 *  08.08.2021  FM  GUI zeigt jetzt die richtigen Items an 
 *                  Problem: Manchmal wird ein Item öfter als 1 mal hinzugefügt
 *  12.08.2021  FM  Problem gefixed
 *  14.08.2021  FM  Kommentare hinzugefügt
 *  17.08.2021  FM  Sript von InventoryGUI zu GUIInventory umbenannt
 *  25.08.2021  FM  OpenMap() und CloseMap() implementiert
 *                  DisplayKeyMissing(), DisplayNotAtGate() hinzugefügt
 *  26.08.2021  FM  DisplayCantUseHealpotion() hinzugefügt
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die visuelle Anzeige des Inventars
/// und die Verwaltung der Inventarstruktur
/// Inventar besteht aus Items
/// </summary>
public class GUIInventory : MonoBehaviour
{
    public Image[] guiInventoryImages;
    private List<Item> inventory = new List<Item>();

    [SerializeField]
    private GameObject mapPart1;
    [SerializeField]
    private GameObject mapPart2;
    [SerializeField]
    private GameObject mapPart3;
    [SerializeField]
    private TextMeshProUGUI textMeshNotAtGate;
    [SerializeField]
    private TextMeshProUGUI textMeshNoKeyInInventory;
    [SerializeField]
    private TextMeshProUGUI textMeshPressSameButtonToContinue;
    [SerializeField]
    private TextMeshProUGUI textMeshCantUseHealpotion;
    [SerializeField]
    private TextMeshProUGUI textCantUseEndurancepotion;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float timer;
    private bool cooldownIsActive = false;

    public int inventoryMaxSize = 5;
    private int itemCount = 0;

    private void Start()
    {
        mapPart1.SetActive(false);
        mapPart2.SetActive(false);
        mapPart3.SetActive(false);
        textMeshNoKeyInInventory.enabled = false;
        textMeshNotAtGate.enabled = false;
        textMeshPressSameButtonToContinue.enabled = false;
        textMeshCantUseHealpotion.enabled = false;
        textCantUseEndurancepotion.enabled = false;
    }

    private void Update()
    {
        UpdateGUI();
        GetInput();
    }


    /// <summary>
    /// Übersetzt den UserInput, damit das dementsprechende Item benutzt werden kann
    /// </summary>
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(4);
        }
    }

    /// <summary>
    /// Debug Log
    /// Zeigt das Inventar in der Console an
    /// </summary>
    private void DisplayInventoryDebug()
    {
        Debug.Log("Inventory Items: ");
        string myStr = "";
        foreach (Item item in inventory)
        {
            myStr += item.itemName + " | ";
        }
        Debug.Log(myStr);
    }
    /// <summary>
    /// Öffnet die Map
    /// </summary>
    /// <param name="_mapInt">Der Map zugewiesene ID</param>
    /// <param name="_indexInInventory">Index der zu öffnenden Map</param>
    public void OpenMap(int _mapInt, int _indexInInventory)
    {
        //Player Position und Rotation einfrieren
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //Anzeige: Drücke dieselbe Taste zum Fortsetzen
        textMeshPressSameButtonToContinue.text = $"Druecke {++_indexInInventory} zum Fortsetzen.";
        textMeshPressSameButtonToContinue.enabled = true;
        Debug.Log("Opening Map");
        //Öffnet die entsprechende Map
        if (_mapInt == 1)
        {
            if (!mapPart1.activeInHierarchy)
            {
                Debug.Log("Setting Map active");
                mapPart1.SetActive(true);
                mapPart2.SetActive(false);
                mapPart3.SetActive(false);
            }
            else
            {
                Debug.Log("Closing Map");
                //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                textMeshPressSameButtonToContinue.enabled = false;
                mapPart1.SetActive(false);
            }
        }
        else if (_mapInt == 2)
        {
            if (!mapPart2.activeInHierarchy)
            {
                Debug.Log("Setting Map active");
                mapPart2.SetActive(true);
                mapPart1.SetActive(false);
                mapPart3.SetActive(false);
            }
            else
            {
                Debug.Log("Closing Map");
                //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                textMeshPressSameButtonToContinue.enabled = false;
                mapPart2.SetActive(false);
            }
        }
        else if (_mapInt == 3)
        {
            if (!mapPart3.activeInHierarchy)
            {
                Debug.Log("Setting Map active");
                mapPart3.SetActive(true);
                mapPart2.SetActive(false);
                mapPart1.SetActive(false);
            }
            else
            {
                Debug.Log("Closing Map");
                //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                textMeshPressSameButtonToContinue.enabled = false;
                mapPart3.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Schließt die Map auf Druck derselben Taste wie zum Öffnen
    /// </summary>
    /// <param name="_mapInt">Der Map zugewiesene ID</param>
    /// <param name="_indexInInventory">Index der zu öffnenden Map</param>
    public void CloseMap(int _mapInt, int _indexInInventory)
    {
        Debug.Log("Closing Map");
        //Spiel fortsetzen bei drücken derselben Taste wie beim Öffnen
        if (Input.GetKeyDown(KeyCode.Alpha1) && _indexInInventory == 0)
        {
            //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //Alle Maps schließen
            mapPart1.SetActive(false);
            mapPart2.SetActive(false);
            mapPart3.SetActive(false);
            textMeshPressSameButtonToContinue.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _indexInInventory == 1)
        {
            //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //Alle Maps schließen
            mapPart1.SetActive(false);
            mapPart2.SetActive(false);
            mapPart3.SetActive(false);
            textMeshPressSameButtonToContinue.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _indexInInventory == 2)
        {
            //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //Alle Maps schließen
            mapPart1.SetActive(false);
            mapPart2.SetActive(false);
            mapPart3.SetActive(false);
            textMeshPressSameButtonToContinue.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && _indexInInventory == 3)
        {
            //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            textMeshPressSameButtonToContinue.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && _indexInInventory == 4)
        {
            //Einfrieren / Pausieren beenden (Rotation bleibt gefreezed)
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            textMeshPressSameButtonToContinue.enabled = false;
        }
    }

    /// <summary>
    /// Der Spieler setzt das Item ein indem er die Taste drückt, an dessen Index das
    /// gewünschte Item liegt. Der Index wird auch in der UI angezeigt
    /// </summary>
    /// <param name="_index">index im Inventar des Items</param>
    private void UseItem(int _index)
    {
        DisplayInventoryDebug();
        Debug.Log("index: " + _index + "inventorySize: " + inventory.Count);
        //Benutzt das Item nicht, wenn keine Items im Inventar sind
        if (_index < inventory.Count)
        {
            Item currentItem = inventory.ElementAt(_index);
            //Null Check
            if (currentItem != null)
            {
                Debug.Log("Using Item...");
                currentItem.Use();
            }
        }
    }


    /// <summary>
    /// Fügt Item zum Inventar hinzu
    /// Beachtet die maximale Größe
    /// </summary>
    /// <param name="_itemToAdd">Das Item welches hinzugefügt wird</param>
    public void AddItem(Item _itemToAdd)
    {
        if (itemCount < inventoryMaxSize)
        {
            inventory.Add(_itemToAdd);
            itemCount++;
        }
        UpdateGUI();
    }

    /// <summary>
    /// Löscht das Item aus dem Inventar
    /// Wenn das Inventar leer ist, passiert nichts.
    /// </summary>
    /// <param name="_itemToRemove">Das Item welches entfernt wird</param>
    public void RemoveItem(Item _itemToRemove)
    {
        if (itemCount > 0)
        {
            inventory.Remove(_itemToRemove);
            itemCount--;
        }
        UpdateGUI();
    }

    /// <summary>
    /// Zeigt an, dass der Spieler den Schlüssel nur beim Gate benutzen darf
    /// </summary>
    public IEnumerator DisplayNotAtGate()
    {
        if (!cooldownIsActive)
        {
            textMeshNotAtGate.enabled = true;
            //2 Sek komplett sichtbar lassen
            yield return new WaitForSeconds(2f);
            Color textColor;
            for (float i = 0f; i < 1f; i += 0.05f)
            {
                textColor = new Color
                    (textMeshNotAtGate.color.r, textMeshNotAtGate.color.g, textMeshNotAtGate.color.b, 1 - i);
                textMeshNotAtGate.color = textColor;
                //Verzögerung um 0.05 sek
                yield return new WaitForSeconds(0.05f);
            }
            textMeshNotAtGate.enabled = false;
            cooldownIsActive = true;
            Invoke("SetCoolDownActiveFalse", 3f);
        }
    }

    /// <summary>
    /// Zeigt an, dass der Spieler einen Schlüssel finden muss,
    /// um das Tor zu öffnen
    /// </summary>
    public IEnumerator DisplayKeyMissing()
    {
        if (!cooldownIsActive)
        {
            textMeshNoKeyInInventory.enabled = true;
            //2 Sek komplett sichtbar lassen
            yield return new WaitForSeconds(2f);
            Color textColor;
            for (float i = 0f; i < 1f; i += 0.05f)
            {
                textColor = new Color
                    (textMeshNoKeyInInventory.color.r, textMeshNoKeyInInventory.color.g, textMeshNoKeyInInventory.color.b, 1 - i);
                textMeshNoKeyInInventory.color = textColor;
                //Verzögerung um 0.05 sek
                yield return new WaitForSeconds(0.05f);
            }
            textMeshNoKeyInInventory.enabled = false;
            cooldownIsActive = true;
            Invoke("SetCoolDownActiveFalse", 3f);
        }
    }

    /// <summary>
    /// Zeigt an, dass der Spieler den Heiltrank nicht benutzen kann
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayCantUseHealpotion()
    {
        if (!cooldownIsActive)
        {
            textMeshCantUseHealpotion.enabled = true;
            yield return new WaitForSeconds(2f);
            Color textColor;
            for (float i = 0f; i < 1f; i += 0.05f)
            {
                textColor = new Color
                    (textMeshCantUseHealpotion.color.r, textMeshCantUseHealpotion.color.g, textMeshCantUseHealpotion.color.b, 1 - i);
                textMeshCantUseHealpotion.color = textColor;
                //Verzögerung um 0.05 sek
                yield return new WaitForSeconds(0.05f);
            }
            textMeshCantUseHealpotion.enabled = false;
            cooldownIsActive = true;
            Invoke("SetCoolDownActiveFalse", 3f);
        }
    }

    public IEnumerator DisplayCantUseEndurancepotion()
    {
        if (!cooldownIsActive)
        {
            textCantUseEndurancepotion.enabled = true;
            yield return new WaitForSeconds(2f);
            Color textColor;
            for (float i = 0f; i < 1f; i += 0.05f)
            {
                textColor = new Color
                    (textCantUseEndurancepotion.color.r, textCantUseEndurancepotion.color.g, textCantUseEndurancepotion.color.b, 1 - i);
                textCantUseEndurancepotion.color = textColor;
                //Verzögerung um 0.05 sek
                yield return new WaitForSeconds(0.05f);
            }
            textCantUseEndurancepotion.enabled = false;
            cooldownIsActive = true;
            Invoke("SetCoolDownActiveFalse", 3f);
        }
    }

    private void SetCoolDownActiveFalse()
    {
        cooldownIsActive = false;
    }
    /// <summary>
    /// Zeigt die Items in der GUI am unteren Bildschirmrand an
    /// </summary>
    private void UpdateGUI()
    {

        for (int i = 0; i < guiInventoryImages.Count(); i++)
        {
            guiInventoryImages[i].enabled = false;
        }

        for (int i = 0; i < inventory.Count(); i++)
        {
            guiInventoryImages[i].enabled = true;
            guiInventoryImages[i].sprite = inventory[i].PItemSprite;
        }
    }

    public List<Item> PGetInventory { get => inventory; }

}
