using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public string ID;
     public EItemType eItemType;
    public void SetID(){
        ID = Guid.NewGuid().ToString();
    }
}


public enum EItemType {
    Computer = 0,
    RecycleBin = 1,
}
