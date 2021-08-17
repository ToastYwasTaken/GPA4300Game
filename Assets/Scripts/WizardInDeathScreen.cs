using UnityEngine;
/******************************************************************************
* Project: GPA4300Game
* File: WizardInDeathScreen.cs
* Version: 1.01
* Autor: Rene Kraus (RK); Franz Moerike (FM); Jan Pagel (JP)
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
*  11.06.2021  FM  erstellt
*  17.08.2021  FM  kommentiert
*            
*****************************************************************************/
public class WizardInDeathScreen : MonoBehaviour
{
    private float speed = 70f;
    [SerializeField]
    private GameObject wizard;

    // Update is called once per frame
    void Update()
    {
        MoveWizardTowardsScreen();
    }

    /// <summary>
    /// Bewegt den Gegner in Richtung der Kamera / "Jump-scae Effekt"
    /// </summary>
    private void MoveWizardTowardsScreen()
    {
        transform.Translate(new Vector3(0f, -0.08f, 1f) * speed * Time.deltaTime);
        if (transform.position.z <= 1f)
        {
            this.gameObject.SetActive(false);
            Destroy(wizard);
        }
    }
}
