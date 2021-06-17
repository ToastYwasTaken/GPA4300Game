using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TrapRocks_Behavior : MonoBehaviour
{
    [SerializeField]
    private GameObject rockPrefab;
    private int rocksNumber = 7;
    [SerializeField]
    private Transform[] spawnPoints;
    private float timeBetweenSpawns = 0.25f;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            for (int i = 0; i < rocksNumber; i++)
            {
                int spawnPosition = Random.Range(0, spawnPoints.Length);
                GameObject rock = Instantiate(rockPrefab);
                rock.transform.Translate(spawnPoints[spawnPosition].position);
            }
        }
    }
}
