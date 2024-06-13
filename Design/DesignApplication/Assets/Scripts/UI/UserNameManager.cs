using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;

public class UserNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button closeItemSelection;
    [SerializeField] private TMP_Dropdown namesDropdown;
    [SerializeField] private Button quitButton;

    private List<string> namesList = new();

    public event Action StartGame;
    bool firstLoad = true;

    private SaveAndLoadSystem saveAndLoadSystem;
    private ItemSpawner itemSpawner;
    public void SetValues(SaveAndLoadSystem saveAndLoadSystem, ItemSpawner itemSpawner){
        this.saveAndLoadSystem = saveAndLoadSystem;
        this.itemSpawner = itemSpawner;
         LoadNames();
        UpdateDropdown();
         namesDropdown.value = 9;
        namesDropdown.onValueChanged.AddListener(OnValueChange);
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        quitButton.onClick.AddListener(QuitGame) ;
        closeItemSelection.onClick.AddListener(CloseItemSelection) ;
    }

    private void CloseItemSelection()
    {
        this.itemSpawner.ItemSelected();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnValueChange(int arg0)
    {
        firstLoad = false;
         LoadNames();
        UpdateDropdown();
       nameInputField.text = namesList[arg0];
    }

    void OnSaveButtonClicked()
    {
        string newName = nameInputField.text;
        if (!string.IsNullOrEmpty(newName))
        {       
            saveAndLoadSystem.SaveNames(newName);
            UpdateDropdown();
            nameInputField.text = "";  
            StartGame?.Invoke();
        }
        else
        {
            Debug.LogWarning("Name is either empty or already exists.");
        }
    }

    void LoadNames()
    {
        namesList = saveAndLoadSystem.LoadNames();
        if(firstLoad)
            namesList.Add("Select Profile");
    }

    void UpdateDropdown()
    {
        namesDropdown.ClearOptions();    
        namesDropdown.AddOptions(namesList);
    }

     void OnDestroy(){
        if(itemSpawner == null) return;
       
        namesDropdown.onValueChanged.RemoveListener(OnValueChange);
        saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        quitButton.onClick.RemoveListener(QuitGame) ;
        closeItemSelection.onClick.RemoveListener(CloseItemSelection) ;
     }
}
