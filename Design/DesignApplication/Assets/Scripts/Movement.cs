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
    private bool gameRunning = false;
    public void SetInputManger(InputManager inputMan){
        gameRunning = true;
        this.inputManager = inputMan;
        inputManager.movmentAction += OnMovemntPressed;
        inputManager.rotationAction += OnRotationPressed;
    }

    private void OnRotationPressed(Vector2 rotation)
    {
        if(!gameRunning) return;
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Quaternion cameraRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        cameraTransform.localRotation = cameraRotation;
    }

    private void OnMovemntPressed(float horizontalMove, float virticalMove )
    {
        if(!gameRunning) return;
        Vector3 move = new Vector3(horizontalMove, 0, virticalMove);
        move = transform.TransformDirection(move);
        move *= MovementSpeed;
        move *= Time.deltaTime;
        transform.position += move;
    }

    private void Start()
    {   
        Vector2 rotation;   
        rotation.y = transform.eulerAngles.y / lookSpeed;
        rotation.x = transform.eulerAngles.x / lookSpeed;
        cameraTransform = Camera.main.transform;
        OnRotationPressed(rotation);
    }

    void OnDestroy(){
        if(!gameRunning) return;
        inputManager.movmentAction -= OnMovemntPressed;
        inputManager.rotationAction -= OnRotationPressed;
    }
}
