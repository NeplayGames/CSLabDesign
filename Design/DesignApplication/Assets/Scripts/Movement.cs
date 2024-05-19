using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 10f;
    [SerializeField] private float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;

    private Transform cameraTransform;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Quaternion cameraRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        cameraTransform.localRotation = cameraRotation;
    }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float virticalMove = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalMove, 0, virticalMove);
        move = transform.TransformDirection(move);
        move *= MovementSpeed;
        move *= Time.deltaTime;

        transform.position += move;
    }
}
