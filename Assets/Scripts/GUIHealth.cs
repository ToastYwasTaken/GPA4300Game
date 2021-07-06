using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    ParticleSystem blood;

    [SerializeField]
    Image flashImage;

    private sbyte phealth;
    private byte defaultHealthColorRed = 95;
    private byte changeGradientRed;

    void Start()
    {
        var emission = blood.emission;
        emission.enabled = false;
        flashImage.enabled = false;
    }

    // Update is called once per frame
    void Update()   
    {
        phealth = player.GetComponent<PlayerHealth>().pHealthProperty; //100 - 0
        healthText.text = phealth.ToString();
        changeGradientRed = (byte)(defaultHealthColorRed + (100 - phealth));
        healthText.color = new Color32(changeGradientRed, 30, 27, byte.MaxValue);
        healthImage.color = new Color32(changeGradientRed, 30, 27, byte.MaxValue);
        if(phealth < 50)
        {
            LightBleeding();
        }
        else if(phealth < 30)
        {
            StartCoroutine("PulseHeart");
            StartCoroutine("Flash");
            StrongBleeding();
        } else if(phealth < 0)
        {
            var emission = blood.emission;
            emission.enabled = false;
            SceneManager.LoadSceneAsync(2);
        }
    }

    private IEnumerator PulseHeart()
    {
        for (float i = 0f; i <= 1f; i += 0.4f)            //TODO
        {
            //Debug.Log(healthImage.transform.localScale);
            healthImage.transform.localScale = new Vector3(
                (Mathf.Lerp(healthImage.transform.localScale.x,
                healthImage.transform.localScale.x + 0.03f,
                Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(healthImage.transform.localScale.y,
                healthImage.transform.localScale.y + 0.03f,
                Mathf.SmoothStep(0f, 1f, i))),
                healthImage.transform.localScale.z);
            yield return new WaitForSeconds(0.02f);
        }
        for (float i = 0f; i <= 1f; i += 0.4f)
        {
            Debug.Log(healthImage.transform.localScale);
            healthImage.transform.localScale = new Vector3(
                (Mathf.Lerp(healthImage.transform.localScale.x,
                healthImage.transform.localScale.x - 0.03f,
                Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(healthImage.transform.localScale.y,
                healthImage.transform.localScale.y - 0.03f,
                Mathf.SmoothStep(0f, 1f, i))),
                healthImage.transform.localScale.z);
            yield return new WaitForSeconds(0.02f);

        }
    }
    
    private void LightBleeding()
    {
        var emission = blood.emission;
        emission.enabled = true;
        emission.rateOverTime = 10f;
    }

    private void StrongBleeding()
    {
        var emission = blood.emission;
        emission.enabled = true;
        emission.rateOverTime = 20f;
    }

    IEnumerator Flash() //TODO
    {
        float iterationAmount = 10f;
        for(float i = 0; i <= iterationAmount; i+= Time.deltaTime)
        {
            Color currentColor = flashImage.color;
            currentColor.a = Mathf.Lerp(0, 1, i/iterationAmount);
            flashImage.color = currentColor;

            yield return null;
        }
    }

}
