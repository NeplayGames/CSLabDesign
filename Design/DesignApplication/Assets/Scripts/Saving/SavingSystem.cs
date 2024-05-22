using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoadSystem 
{
    
    public string SavingSystemName = Application.persistentDataPath + "/SaveInfo";

    public SaveAndLoadSystem()
    {
        LoadAllItems();
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

      void LoadAndInstantiatePrefab(ItemData prefabData)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabData.name);

        if (prefab != null)
        {
           Item item = GameObject.Instantiate(prefab, prefabData.position, 
           Quaternion.Euler(prefabData.rotation)).GetComponent<Item>();
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

    public void SavePrefab(string name, string id, Vector3 position, Vector3 rotation)
    {

        List<ItemData> data = GetDatas();
        data.RemoveAll(dat => dat.ID == id);
        ItemData prefabData = new ItemData
        {
            name = name,
            ID = id,
            position = position,
            rotation = rotation
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

    [System.Serializable]
    public class ItemData
    {
        public string name;
        public string ID;
        public Vector3 position;
        public Vector3 rotation;
    }

    [System.Serializable]
    public class ItemDatasWrapper{
        public List<ItemData> data = new();
    }
}
