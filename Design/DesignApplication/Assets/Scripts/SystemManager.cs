using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Movement movement;
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform itemButtonParents;
    private InputManager inputManager;
    private SaveAndLoadSystem savingSystem;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = new();
        savingSystem = new();
        movement.SetInputManger(inputManager);
        SetItems(itemDataBase);
    }

    private void ShowUI(bool showUI)
    {
        
    }

    private void SetItems(ItemDataBase itemDataBase)
    {
        itemSpawner.SetInputManger(inputManager, savingSystem);
        int i = 0;
        foreach(var item in itemDataBase.eachItems){
            ItemButton itemButton = Instantiate(itemDataBase.itemButton, itemButtonParents).GetComponent<ItemButton>();
            itemButton.SetItem( item.itemSprite, item.prefab, itemSpawner, i);
            i++;
        }
    }

    void Update(){
        inputManager.Run();
    }
    void OnDestroy(){
    }
}
