using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 10f;
    [SerializeField] private float lookSpeed = 3;

    private Transform cameraTransform;

    private InputManager inputManager;

    public void SetInputManger(InputManager inputMan){
        this.inputManager = inputMan;
        inputManager.movmentAction += OnMovemntPressed;
        inputManager.rotationAction += OnRotationPressed;
    }

    private void OnRotationPressed(Vector2 rotation)
    {
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Quaternion cameraRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        cameraTransform.localRotation = cameraRotation;
    }

    private void OnMovemntPressed(float horizontalMove, float virticalMove )
    {
        Vector3 move = new Vector3(horizontalMove, 0, virticalMove);
        move = transform.TransformDirection(move);
        move *= MovementSpeed;
        move *= Time.deltaTime;
        transform.position += move;
    }

    private void Start()
    {      
        cameraTransform = Camera.main.transform;
    }

    void OnDestroy(){
        inputManager.movmentAction -= OnMovemntPressed;
        inputManager.rotationAction -= OnRotationPressed;
    }
}
