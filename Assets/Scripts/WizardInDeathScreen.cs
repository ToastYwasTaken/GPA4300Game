using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInDeathScreen : MonoBehaviour
{
    private float speed = 70f;
    [SerializeField]
    private GameObject wizard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, -0.08f, 1f) * speed * Time.deltaTime);

        if(transform.position.z <= 1f)
        {
            this.gameObject.SetActive(false);
            Destroy(wizard);
        }
    }
}
