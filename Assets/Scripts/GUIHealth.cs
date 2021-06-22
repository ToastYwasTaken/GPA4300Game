using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/******************************************************************************
 * Project: GPA4300Game
 * File: GUIHealth.cs
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
 *  11.06.2021  FM  Created
 *  22.06.2021  FM  fixed issue that caused not displaying the healthCount
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die Lebensanzeige im Canvas
/// </summary>
 
public class GUIHealth : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private Image healthImage;

    private sbyte phealth;
    private byte defaultHealthColorRed = 95;
    private byte changeGradientRed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        phealth = player.GetComponent<PlayerHealth>().pHealthProperty; //100 - 0
        healthText.text = phealth.ToString();
        changeGradientRed = (byte)(defaultHealthColorRed + (100 - phealth));
        healthText.color = new Color32(changeGradientRed, 30, 27, byte.MaxValue);
        healthImage.color = new Color32(changeGradientRed, 30, 27, byte.MaxValue);
        if(phealth < 30)
        {
            for(float i = 0f; i <= 1f; i+= 0.1f)            //TODO doesnt work yet
            {
                healthImage.transform.localScale = new Vector3(
                    (Mathf.Lerp(healthImage.transform.localScale.x, 
                    healthImage.transform.localScale.x + 0.025f, 
                    Mathf.SmoothStep(0f, 1f, i))),
                    (Mathf.Lerp(healthImage.transform.localScale.y, 
                    healthImage.transform.localScale.y + 0.025f, 
                    Mathf.SmoothStep(0f, 1f, i))), 
                    healthImage.transform.localScale.z);
            }
            for (float i = 0f; i <= 1f; i += 0.1f)
            {
                healthImage.transform.localScale = new Vector3(
                    (Mathf.Lerp(healthImage.transform.localScale.x,
                    healthImage.transform.localScale.x - 0.025f,
                    Mathf.SmoothStep(0f, 1f, i))),
                    (Mathf.Lerp(healthImage.transform.localScale.y,
                    healthImage.transform.localScale.y - 0.025f,
                    Mathf.SmoothStep(0f, 1f, i))),
                    healthImage.transform.localScale.z);
            }
        }
    }
}
