using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private List<Transform> respawnPoints;

    private void SpawnPLayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoints[Random.Range(0, respawnPoints.Count)].transform.position;
            Physics.SyncTransforms();
        }
    }
}
