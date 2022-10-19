using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public GameObject centerCube;

    void Update()
    {
        Orbit(centerCube);
    }

    void Orbit(GameObject gameObject)
    {
        transform.RotateAround(gameObject.transform.position, transform.up, 20 * Time.deltaTime);
    }
}
