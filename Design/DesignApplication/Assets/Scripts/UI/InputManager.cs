using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
   public event Action<float, float> movmentAction;
   public event Action<Vector2> rotationAction;
    public event Action onMouseLeftClick;
    public event Action<bool> onXClick;
    public event Action<bool> onYClick;
    public event Action<bool> onZClick;
    public event Action onMouseRightClick;
    public event Action objectRotation;
    public event Action ShowButtons;
    public event Action reloadScene;
    public event Action<Vector3> mousePositionEvent;
    public event Action<float> mouseScrollChange;
    Vector2 rotation = Vector2.zero;
    Vector3 mousePosition;
    private float mouseScrollPosition;
    public InputManager()
    {
        mouseScrollPosition = Input.GetAxis("Mouse ScrollWheel");
    }

    public void Run(){
        HandleReload();
        HandleShowButtons();
        HandleXYZClick();
        HandleMouseLeftClick();
        handleMousePosition();
        HandleMouseRightClick();
        HandleMovement();
        HandleMouseScrollPosition();
        HandleCameraRotation();
        HandleObjectRotation();
   }

    private void HandleXYZClick()
    {
         if(Input.GetKeyDown(KeyCode.X))
            onXClick?.Invoke(true);
            if(Input.GetKeyDown(KeyCode.Y))
            onYClick?.Invoke(true);
            if(Input.GetKeyDown(KeyCode.Z))
            onZClick?.Invoke(true);
            if(Input.GetKeyUp(KeyCode.X))
            onXClick?.Invoke(false);
            if(Input.GetKeyUp(KeyCode.Y))
            onYClick?.Invoke(false);
            if(Input.GetKeyUp(KeyCode.Z))
            onZClick?.Invoke(false);
    }

    private void HandleShowButtons()
    {
         if(Input.GetKeyDown(KeyCode.Tab)){
            ShowButtons?.Invoke();
         }
    }

    private void HandleMouseScrollPosition()
    {
        float currentScrollPosition = Input.GetAxis("Mouse ScrollWheel");;
        if(currentScrollPosition != mouseScrollPosition){
            mouseScrollChange?.Invoke(currentScrollPosition);
            mouseScrollPosition = currentScrollPosition;
        }
    }

    private void HandleReload()
    {
          if(Input.GetKeyDown(KeyCode.Escape))
            reloadScene?.Invoke();
    }

    private void HandleObjectRotation()
    {
        if(Input.GetKeyDown(KeyCode.R))
            objectRotation?.Invoke();
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
