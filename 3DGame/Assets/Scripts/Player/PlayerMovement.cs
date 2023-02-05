using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Game Systems/Player/Movement")]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charC;

    [SerializeField]
    private Transform cameraTransform;

    [Header("Movement Speeds")]
    public float speed = 50f;
    public float jumpSpeed = 50f, gravity = 20f, walk = 10f, run = 50f;

    void Start()
    {
        _charC = GetComponent<CharacterController>();
    }

    void Update()
    {
        //checks if the game is in 'game' mode
        if (MenuHandler.menuHandlerInstance.gameState == GameStates.GameState) 
        {
            //checks if the player model is on the ground
            if (_charC.isGrounded)
            {
                //moves the player model according to the player movement inputs
                _moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * speed;
                //rotates the player towards the direction the camera is facing
                _moveDirection = Quaternion.AngleAxis(cameraTransform.transform.rotation.eulerAngles.y, Vector3.up) * _moveDirection;   

                //checks if the player has pressed the jump button
                if (Input.GetButton("Jump"))
                {
                    //moves the player along the y axis at jump speed
                    _moveDirection.y = jumpSpeed;
                }
            }

            //if player is not on the ground, moves the player downwards at a set speed, simulating gravity
            _moveDirection.y -= gravity * Time.deltaTime;
            //moves the player according to the movement inputs and camera direction
            _charC.Move(_moveDirection * Time.deltaTime);
        }
    }
}
