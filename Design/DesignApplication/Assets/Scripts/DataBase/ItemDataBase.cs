using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "DataBase/Item", order = 0)]
public class ItemDataBase: ScriptableObject {

    public List<EachItems> eachItems = new();
    public GameObject itemButton;
    [System.Serializable]
    public class EachItems 
    {
        [field: SerializeField] public Sprite itemSprite {get; private set;} 
        [field: SerializeField] public GameObject prefab {get; private set;} 
    }
}


