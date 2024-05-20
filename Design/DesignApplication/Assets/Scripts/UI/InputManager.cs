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
    private bool isUIShown = true;
    public event Action<bool> showUI;
    public event Action<Vector3> mousePositionEvent;
    Vector2 rotation = Vector2.zero;
    Vector3 mousePosition;
    public InputManager()
    {
        showUI?.Invoke(isUIShown);
    }

    public void Run(){
       // ShowUI();
        HandleMouseLeftClick();
        handleMousePosition();
        if(isUIShown)return;
        HandleMovement();
        HandleRotation();
        HandleMouseRightClick();

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

    private void HandleRotation()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x -= Input.GetAxis("Mouse Y");
        rotationAction?.Invoke(rotation);
    }

    private void ShowUI()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            EnableUI();
        }
    }

    public void EnableUI()
    {
        isUIShown = !isUIShown;
        showUI?.Invoke(isUIShown);
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
