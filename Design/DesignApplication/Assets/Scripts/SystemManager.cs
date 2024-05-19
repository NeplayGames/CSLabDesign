using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Transform itemButtonParents;

    // Start is called before the first frame update
    void Start()
    {
        SetItems(itemDataBase);
    }

    private void SetItems(ItemDataBase itemDataBase)
    {
        foreach(var item in itemDataBase.eachItems){
            ItemButton itemButton = Instantiate(itemDataBase.itemButton, itemButtonParents).GetComponent<ItemButton>();
            itemButton.SetItem(item.itemSprite, item.prefab, itemSpawner);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
