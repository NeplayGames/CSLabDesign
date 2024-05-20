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
            List<PrefabData> data = JsonUtility.FromJson<PrefabDatasWrapper>(j).data;
            foreach (PrefabData dataItem in data){
                LoadAndInstantiatePrefab(dataItem);
            }
        }
    }

      void LoadAndInstantiatePrefab(PrefabData prefabData)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabData.name);

        if (prefab != null)
        {
           GameObject.Instantiate(prefab, prefabData.position, Quaternion.Euler(prefabData.rotation));
        }
        else
        {
            Debug.LogError("Prefab not found: " + prefabData.name);
        }
    }

    public void SavePrefab(string name, Vector3 position, Vector3 rotation){

        List<PrefabData> data = new();
        if (File.Exists(SavingSystemName))
        {
            // Read the existing data from the file
            string j = File.ReadAllText(SavingSystemName);
            data = JsonUtility.FromJson<PrefabDatasWrapper>(j).data;
        }

        PrefabData prefabData = new PrefabData
        {
            name = name,
            position = position,
            rotation = rotation
        };

        PrefabDatasWrapper prefabDatasWrapper= new PrefabDatasWrapper{
            data = data,
        };
        
        data.Add(prefabData);
        Debug.Log(data);
        string json = JsonUtility.ToJson(prefabDatasWrapper, true);
        File.WriteAllText(SavingSystemName , json);
        Debug.Log($"The file is store in {SavingSystemName}");
    }

    [System.Serializable]
    public class PrefabData
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
    }

    [System.Serializable]
    public class PrefabDatasWrapper{
        public List<PrefabData> data = new();
    }
}
