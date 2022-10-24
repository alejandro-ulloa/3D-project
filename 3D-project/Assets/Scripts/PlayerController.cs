using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] Camera playerCamera;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float gravity = 9.81f;

    CharacterController controller;

    Vector3 curMoveInput;


    void Start() {
        try {
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

            if (moveSpeed <= 0.0f) {
                moveSpeed = 6.0f;
                throw new ArgumentNullException("moveSpeed argument is null so the value has been defaulted to 6.0");
            }

            if (gravity <= 0.0f) {
                gravity = 9.81f;
                throw new ArgumentNullException("gravity argument is null so the value has been defaulted to 9.81f");
            }
        } catch (ArgumentNullException e) {
            Debug.Log(e.Message);
        } finally {
            Debug.Log("moveSpeed validation always runs");
        }
    }

    void Update() {
        if (controller.isGrounded) {
            curMoveInput = transform.TransformDirection(curMoveInput);
        }

        curMoveInput.y -= gravity * Time.deltaTime;
        controller.Move(curMoveInput * Time.deltaTime);
    }

    public void MovePlayer(InputAction.CallbackContext context) {
        Vector2 move = context.action.ReadValue<Vector2>();
        move.Normalize();

        Vector3 moveVel = new Vector3(move.x, 0, move.y);
        moveVel *= moveSpeed;
        try {
            curMoveInput = moveVel;
            if (curMoveInput == null) {
                throw new ArgumentNullException("curMoveInput argument is null");
            }
        } catch (ArgumentNullException e) {
            Debug.Log(e.Message);
        }
        
    }

    public void Look(InputAction.CallbackContext context) {

    }

    public void Fire(InputAction.CallbackContext context) {

    }
}