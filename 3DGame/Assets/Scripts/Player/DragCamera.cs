using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    public Transform target;
    public float mouseSensitivity = 3f;

    private float _distanceFromTarget = 10f;
    private float _smoothStrength = 0.1f;
    private float _rotX;
    private float _rotY;

    Vector3 _smoothVelocity = Vector3.zero;
    Vector3 _currentRotation;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _rotY += _mouseX;
            _rotX -= _mouseY;

            _rotX = Mathf.Clamp(_rotX, -45, 90);

            Vector3 _nextRotation = new Vector3(_rotX, _rotY);
            _currentRotation = Vector3.SmoothDamp(_currentRotation, _nextRotation, ref _smoothVelocity, _smoothStrength);
            transform.localEulerAngles = _currentRotation;

            transform.position = target.position - transform.forward * _distanceFromTarget;
        }
    }
}
