using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
   public event Action<float, float> movmentAction;
   public event Action<Vector2> rotationAction;
    public event Action onMouseLeftClick;
    public event Action onMouseRightClick;
    public event Action objectRotation;
    public event Action reloadScene;
    public event Action<Vector3> mousePositionEvent;
    Vector2 rotation = Vector2.zero;
    Vector3 mousePosition;

    public void Run(){
        HandleQuit();
        HandleReload();
        HandleMouseLeftClick();
        handleMousePosition();
        HandleMouseRightClick();
        HandleMovement();
        HandleCameraRotation();
        HandleObjectRotation();
   }

    private void HandleReload()
    {
          if(Input.GetKeyDown(KeyCode.Tab))
            reloadScene?.Invoke();
    }

    private void HandleObjectRotation()
    {
        if(Input.GetKeyDown(KeyCode.R))
            objectRotation?.Invoke();
    }

    private void HandleQuit()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    private void handleMousePosition()
    {
       if(mousePosition != Input.mousePosition){
        mousePosition = Input.mousePosition;
        mousePositionEvent?.Invoke(mousePosition);
       }
    }

    private void HandleMouseRightClick()
    {
        if(Input.GetMouseButtonDown(1))
        onMouseRightClick?.Invoke();
    }

    private void HandleMouseLeftClick()
    {
         if(Input.GetMouseButtonDown(0))
        onMouseLeftClick?.Invoke();
    }

    private void HandleCameraRotation()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x -= Input.GetAxis("Mouse Y");
        rotationAction?.Invoke(rotation);
    }

    private void HandleMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float virticalMove = Input.GetAxis("Vertical");
        if(horizontalMove != 0 || virticalMove != 0){
            movmentAction?.Invoke(horizontalMove, virticalMove);
        }
    }
}
