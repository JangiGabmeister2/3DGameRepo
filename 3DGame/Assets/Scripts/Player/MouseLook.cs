using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationalAxis
    {
        MouseX, MouseY
    }

    [Header("Rotation")]
    public RotationalAxis axis = RotationalAxis.MouseX;

    [Header("Sensitivity")]
    public Vector2 sensitivity = new Vector2(10, 10);
    [Range(-100, 100)]

    public float minY = -60f, maxY = 60f;
    public bool invert;

    private float _rotY;

    private void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        if (GetComponent<Camera>())
        {
            axis = RotationalAxis.MouseY;
        }
    }

    private void Update()
    {
        if (MenuHandler.menuHandlerInstance.gameState == GameStates.GameState)
        {
            if (axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x, 0);
            }
            else
            {
                _rotY += Input.GetAxis("Mouse Y") * sensitivity.y;
                _rotY = Mathf.Clamp(_rotY, minY, maxY);

                if (invert)
                {
                    transform.localEulerAngles = new Vector3(_rotY, 0, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
                }
            }
        }
    }
}
