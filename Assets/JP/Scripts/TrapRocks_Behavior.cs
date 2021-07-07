using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

/*
 * Regelt das Verhalten der Falle TrapRocks und lässt Steine von der Decke fallen
 * Author: JP
 */
public class TrapRocks_Behavior : MonoBehaviour
{
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private int numberOfRocks;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float deflectionForce;
    [SerializeField]
    private float timeBetweenSpawns;

    private IEnumerator spawnRocks()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            // Lässt einen Stein an einem zufälligen Spawnpoint entstehen
            int spawnPosition = Random.Range(0, spawnPoints.Length);
            GameObject rock = Instantiate(rockPrefab);
            rock.transform.Translate(spawnPoints[spawnPosition].position);

            //Gibt den Steinen eine Ablenkung, damit sie sich nicht stapeln
            Rigidbody rockRB = rock.GetComponent<Rigidbody>();
            int deflection = Random.Range(0, 4);
            switch (deflection)
            {
                case 0:
                    rockRB.AddForce(deflectionForce, 0, 0);
                    break;
                case 1:
                    rockRB.AddForce(-deflectionForce, 0, 0);
                    break;
                case 2:
                    rockRB.AddForce(0, 0, deflectionForce);
                    break;
                case 3:
                    rockRB.AddForce(0, 0, -deflectionForce);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            StartCoroutine(spawnRocks());
        }
    }
}
