using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TrapRocks_Behavior : MonoBehaviour
{
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private int numberOfRocks;
    //[SerializeField]
    public Transform[] spawnPoints;
    [SerializeField]
    private float deflectionForce;
    [SerializeField]
    private float timeBetweenSpawns;

    private IEnumerator spawnRocks()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            int spawnPosition = Random.Range(0, spawnPoints.Length);
            int deflection = Random.Range(0, 4);

            GameObject rock = Instantiate(rockPrefab);
            rock.transform.Translate(spawnPoints[spawnPosition].position);

            Rigidbody rockRB = rock.GetComponent<Rigidbody>();
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
