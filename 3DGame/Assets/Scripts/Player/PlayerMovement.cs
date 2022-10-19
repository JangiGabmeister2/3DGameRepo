using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Game Systems/Player/Movement")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MenuHandler))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Character")]
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charC;

    [Header("Movement Speeds")]
    public float speed = 50f;
    public float jumpSpeed = 50f, gravity = 20f, walk = 10f, run = 50f;

    void Start()
    {
        _charC = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (MenuHandler.menuHandlerInstance.gameState == GameStates.GameState)
        {
            if (_charC.isGrounded)
            {
                _moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal Key"), 0, Input.GetAxis("Vertical Key"))) * speed;

                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = jumpSpeed;
                }
            }

            _moveDirection.y -= gravity * Time.deltaTime;
            _charC.Move(_moveDirection * Time.deltaTime);
        }
    }
}
