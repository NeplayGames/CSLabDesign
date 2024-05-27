using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UserNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_Dropdown namesDropdown;

    private List<string> namesList = new();

    private SaveAndLoadSystem saveAndLoadSystem;
    public void SetValues(SaveAndLoadSystem saveAndLoadSystem){
        this.saveAndLoadSystem = saveAndLoadSystem;
         LoadNames();
        UpdateDropdown();

        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    void OnSaveButtonClicked()
    {
        string newName = nameInputField.text;
        if (!string.IsNullOrEmpty(newName) && !namesList.Contains(newName))
        {       
            saveAndLoadSystem.SaveNames(newName);
            UpdateDropdown();
            nameInputField.text = "";  
        }
        else
        {
            Debug.LogWarning("Name is either empty or already exists.");
        }
    }

    void LoadNames()
    {
        namesList = saveAndLoadSystem.LoadNames();
    }

    void UpdateDropdown()
    {
        namesDropdown.ClearOptions();
        namesDropdown.AddOptions(namesList);
    }
}
