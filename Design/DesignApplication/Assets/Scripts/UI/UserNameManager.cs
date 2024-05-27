using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class UserNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_Dropdown namesDropdown;

    private List<string> namesList = new();

    public event Action StartGame;
    bool firstLoad = true;

    private SaveAndLoadSystem saveAndLoadSystem;
    public void SetValues(SaveAndLoadSystem saveAndLoadSystem){
        this.saveAndLoadSystem = saveAndLoadSystem;
         LoadNames();
        UpdateDropdown();
         namesDropdown.value = 9;
        namesDropdown.onValueChanged.AddListener(OnValueChange);
        saveButton.onClick.AddListener(OnSaveButtonClicked);
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
            namesList.Add("Select User");
    }

    void UpdateDropdown()
    {
        namesDropdown.ClearOptions();    
        namesDropdown.AddOptions(namesList);
    }
}
