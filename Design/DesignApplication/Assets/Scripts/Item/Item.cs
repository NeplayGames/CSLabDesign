using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    [HideInInspector] public string ID;
     public string itemName;

    public EItemType eItemType {get; private set;}
    private Vector3 size;
    private float currentSize;
    public void SetID(){
        ID = Guid.NewGuid().ToString();
    }
    
    private void Start(){
        size = transform.localScale;
        currentSize = 1;
    }

    public void ChangeSize(float delta){
        this.currentSize += delta;
        transform.localScale = size * currentSize;
    }

}

public enum EItemType{
    floorType,
    ceilingType,
}

