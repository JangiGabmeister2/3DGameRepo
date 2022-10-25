using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitAround : MonoBehaviour
{
    [Header("Center Object")]
    public GameObject centerCube;

    private int defaultValue = 20;

    [Header("UI Elements")]
    public Slider speedModifier;
    public Text speedValue;
    public Button defaultValueButton;
    public Toggle invertDirection;

    void Update()
    {
        Orbit(centerCube);
        speedValue.text = speedModifier.value.ToString();
    }

    public void DefaultValue()
    {
        speedModifier.value = defaultValue;
    }

    void Orbit(GameObject gameObject)
    {
        if (!invertDirection.isOn)
        {
            transform.RotateAround(gameObject.transform.position, transform.up, speedModifier.value * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(gameObject.transform.position, transform.up, -speedModifier.value * Time.deltaTime);
        }
    }
}
