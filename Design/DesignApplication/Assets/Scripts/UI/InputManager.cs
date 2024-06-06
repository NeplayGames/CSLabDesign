using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
   public event Action<float, float> MovmentAction;
   public event Action<Vector2> RotationAction;
    public event Action OnMouseLeftClick;
    public event Action<bool> OnXClick;
    public event Action<bool> OnYClick;
    public event Action<bool> OnZClick;
    public event Action OnMouseRightClick;
    public event Action ObjectRotation;
    public event Action ShowButtons;
    public event Action ReloadScene;
    public event Action<bool> ShowCutScene;
    public event Action<Vector3> MousePositionEvent;
    public event Action<float> MouseScrollChange;
    Vector2 rotation = Vector2.zero;
    Vector3 mousePosition;
    private float mouseScrollPosition;

    private bool showCutScene = false;
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
        HandleCutScene();
   }

    private void HandleCutScene()
    {
         if(Input.GetKeyDown(KeyCode.C)){
            showCutScene = !showCutScene;
            ShowCutScene?.Invoke(showCutScene);
         }
    }

    private void HandleXYZClick()
    {
         if(Input.GetKeyDown(KeyCode.X))
            OnXClick?.Invoke(true);
            if(Input.GetKeyDown(KeyCode.Y))
            OnYClick?.Invoke(true);
            if(Input.GetKeyDown(KeyCode.Z))
            OnZClick?.Invoke(true);
            if(Input.GetKeyUp(KeyCode.X))
            OnXClick?.Invoke(false);
            if(Input.GetKeyUp(KeyCode.Y))
            OnYClick?.Invoke(false);
            if(Input.GetKeyUp(KeyCode.Z))
            OnZClick?.Invoke(false);
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
            MouseScrollChange?.Invoke(currentScrollPosition);
            mouseScrollPosition = currentScrollPosition;
        }
    }

    private void HandleReload()
    {
          if(Input.GetKeyDown(KeyCode.Escape))
            ReloadScene?.Invoke();
    }

    private void HandleObjectRotation()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ObjectRotation?.Invoke();
    }

  

    private void handleMousePosition()
    {
       if(mousePosition != Input.mousePosition){
        mousePosition = Input.mousePosition;
        MousePositionEvent?.Invoke(mousePosition);
       }
    }

    private void HandleMouseRightClick()
    {
        if(Input.GetMouseButtonDown(1))
        OnMouseRightClick?.Invoke();
    }

    private void HandleMouseLeftClick()
    {
         if(Input.GetMouseButtonDown(0))
        OnMouseLeftClick?.Invoke();    
    }

    private void HandleCameraRotation()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x -= Input.GetAxis("Mouse Y");
        RotationAction?.Invoke(rotation);
    }

    private void HandleMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float virticalMove = Input.GetAxis("Vertical");
        if(horizontalMove != 0 || virticalMove != 0){
            MovmentAction?.Invoke(horizontalMove, virticalMove);
        }
    }
}
