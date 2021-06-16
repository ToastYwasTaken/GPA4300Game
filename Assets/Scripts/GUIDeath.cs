using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIDeath : MonoBehaviour
{
    public TextMeshProUGUI textOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for (float i = 0f; i < 1f; i+= 0.05f)
        {
            Color textColor = new Color
                (textOnDeath.color.r, textOnDeath.color.g, textOnDeath.color.b, i);
            textOnDeath.color = textColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
