using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    [HideInInspector] public string ID;
    [HideInInspector] public string itemName;

    private Vector3 size;
    private float currentSizeX;
    private float currentSizeY;
    private float currentSizeZ;
    public void SetID(){
        ID = Guid.NewGuid().ToString();
    }
    
    private void Start(){
        size = transform.localScale;
        currentSizeX = 1;
        currentSizeY = 1;
        currentSizeZ = 1;
    }

    public void ChangeSize(float deltaX,float deltaY,float deltaZ){
        this.currentSizeX += deltaX;
        this.currentSizeY += deltaY;
        this.currentSizeZ += deltaZ;
        transform.localScale = new Vector3(size.x * currentSizeX, size.y * currentSizeY, size.z * currentSizeZ);
    }
}

