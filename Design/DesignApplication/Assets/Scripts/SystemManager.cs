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
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform itemButtonParents;
    private InputManager inputManager;
    private SaveAndLoadSystem savingSystem;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = new();
        savingSystem = new();
        userNameManager.StartGame += StartGame;
        inputManager.reloadScene += ReloadGame;
         userNameManager.SetValues(savingSystem);

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
          Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
        SetItems(itemDataBase);
        userNameManager.gameObject.SetActive(false);
    }

    private void SetItems(ItemDataBase itemDataBase)
    {
        itemSpawner.SetInputManger(inputManager, savingSystem);
        int i = 1;
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
        userNameManager.StartGame -= StartGame;
        inputManager.reloadScene -= ReloadGame;
    }
}
