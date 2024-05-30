using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class SaveAndLoadSystem 
{
    private string userName;
    public string SavingSystemName  {get { return Path.Combine(Application.dataPath, "Resources", $"{userName}SaveInfo"); }}
    public string userSystemName = Path.Combine(Application.dataPath, "Resources", "UserInfo");

    public SaveAndLoadSystem()
    {
    }

    private void LoadAllItems()
    {
        if (File.Exists(SavingSystemName))
        {
            // Read the existing data from the file
            string j = File.ReadAllText(SavingSystemName);
            List<ItemData> data = JsonUtility.FromJson<ItemDatasWrapper>(j).data;
            foreach (ItemData dataItem in data){
                LoadAndInstantiatePrefab(dataItem);
            }
        }
    }

     public void SaveNames(string name)
    {
       var names = LoadNames();
       if(!names.Contains(name))
            names.Add(name);
       userName = name;
       string jsonData = JsonUtility.ToJson(new NamesList(names), true);
       File.WriteAllText(userSystemName, jsonData);
       Debug.Log($"FIles name written {names.Count}");
        LoadAllItems();
    }

    public List<string> LoadNames()
    {
         if (File.Exists(userSystemName))
        {
            // Read the existing data from the file
            string j = File.ReadAllText(userSystemName);
            Debug.Log(j);
             NamesList namesList = JsonUtility.FromJson<NamesList>(j);
         Debug.Log($"FIles name written {namesList.names.Count}");

            return namesList.names;
        }                
        return new List<string>();     
    }
      void LoadAndInstantiatePrefab(ItemData prefabData)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabData.name);

        if (prefab != null)
        {
           Item item = GameObject.Instantiate(prefab, prefabData.position, 
           Quaternion.Euler(prefabData.rotation)).GetComponent<Item>();
           item.transform.localScale = prefabData.scale;
           item.ID = prefabData.ID;
           
        }
        else
        {
            Debug.LogError("Prefab not found: " + prefabData.ID);
        }
    }

    public void RemoveItem(string id)
    {
        List<ItemData> data = GetDatas();
        data.RemoveAll(dat => dat.ID == id);
        SaveData(data);
    }

    private List<ItemData> GetDatas()
    {
        List<ItemData> data = new();
        if (File.Exists(SavingSystemName))
        {
            // Read the existing data from the file
            string j = File.ReadAllText(SavingSystemName);
            data = JsonUtility.FromJson<ItemDatasWrapper>(j).data;
        }

        return data;
    }

    public void SavePrefab(string name, string id, Vector3 position, Vector3 rotation,Vector3 scale)
    {

        List<ItemData> data = GetDatas();
        data.RemoveAll(dat => dat.ID == id);
        ItemData prefabData = new ItemData
        {
            name = name,
            ID = id,
            position = position,
            rotation = rotation, 
            scale = scale
        };
        data.Add(prefabData);

        SaveData(data);
    }

    private void SaveData(List<ItemData> data)
    {
        ItemDatasWrapper prefabDatasWrapper = new ItemDatasWrapper
        {
            data = data,
        };

        Debug.Log(data);
        string json = JsonUtility.ToJson(prefabDatasWrapper, true);
        File.WriteAllText(SavingSystemName, json);
        Debug.Log($"The file is store in {SavingSystemName}");
    }

    // Use this method to load data from a JSON file in the Resources folder
    [System.Serializable]
    public class ItemData
    {
        public string name;
        public string ID;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    [System.Serializable]
    public class ItemDatasWrapper{
        public List<ItemData> data = new();
    }
    
    [System.Serializable]
    private class NamesList
    {
        public List<string> names;

        public NamesList(List<string> names)
        {
            this.names = names;
        }
    }
}
