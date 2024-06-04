using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private UserNameManager userNameManager;
    [SerializeField] private Movement movement;
    [SerializeField] private GameObject itemButtonParents;
    private InputManager inputManager;
    private SaveAndLoadSystem savingSystem;
    public bool runGame = false;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = new();
        savingSystem = new();
        userNameManager.StartGame += StartGame;
        inputManager.reloadScene += ReloadGame;
         userNameManager.SetValues(savingSystem);
        inputManager.ShowButtons += ShowButtons;
        itemSpawner.NewItemSelected += NewItemSelected;
    }

    private void NewItemSelected()
    {
        itemButtonParents.SetActive(false);
        runGame = true;
        DeactiveCursor();
    }

    private void ReloadGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartGame()
    {
        movement.SetInputManger(inputManager);
        DeactiveCursor();
        SetItems(itemDataBase);
        userNameManager.gameObject.SetActive(false);
        runGame = true;
    }

    private void DeactiveCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void ActiveCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ShowButtons()
    {
       itemButtonParents.SetActive(true);
       runGame = false;
       ActiveCursor();
    }

    private void SetItems(ItemDataBase itemDataBase)
    {
        itemSpawner.SetInputManger(inputManager, savingSystem);
        foreach(var item in itemDataBase.eachItems){
            ItemButton itemButton = Instantiate(itemDataBase.itemButton, itemButtonParents.transform).GetComponent<ItemButton>();
            item.prefab.itemName = item.prefab.name;
            itemButton.SetItem( item.itemSprite, item.prefab, itemSpawner);
        }
    }

    void Update(){
        if(runGame)
            inputManager.Run();
    }

    void OnDestroy(){
        userNameManager.StartGame -= StartGame;
        inputManager.reloadScene -= ReloadGame;
        inputManager.ShowButtons -= ShowButtons;

    }
}
