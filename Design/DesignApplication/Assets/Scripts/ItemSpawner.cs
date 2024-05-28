using System;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private LayerMask itemLayer;
    private GameObject prefab;
    private InputManager inputManager;
    private SaveAndLoadSystem savingSystem;
    private string currentItemName;
    private string ID;    
    private void MovePreview(Vector3 mousePosition)
    {
        if(prefab == null) return; 
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, layer))
        {
            prefab.transform.position = new Vector3(
                        Mathf.Round(hit.point.x),
                        Mathf.Round(hit.point.y) + hit.transform.localScale.y * .5f,
                        Mathf.Round(hit.point.z)
                    );
        }
    }

    public void Build(Item template) 
    {
        if(prefab){
            Destroy(prefab);
        }
        currentItemName = template.name;
        template.Do(x => {
            x.SetID();
            prefab = Instantiate(template.gameObject);
            ID = x.ID;
        }
        );
    }
    
    public void Rotate() => prefab?.transform.Rotate(new Vector3(0, 90, 0), Space.Self);

    public void SetInputManger(InputManager inputMan, SaveAndLoadSystem savingSystem)
    {
        this.savingSystem = savingSystem;
        this.inputManager = inputMan;
        inputManager.mousePositionEvent += MovePreview;
        inputManager.onMouseLeftClick += AddItem;
        inputManager.onMouseRightClick += DeleteItem;
        inputManager.objectRotation += Rotate;
    }

    private void DeleteItem()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, itemLayer))
        {
            prefab = hit.transform.gameObject;         
        }
        if(prefab == null)
            return;  
        Item item = prefab.GetComponent<Item>();
        savingSystem.RemoveItem(item.ID);
        Destroy(prefab);
        prefab = null;
    }

    private void AddItem()
    {
         if(prefab == null)
         {
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30f, itemLayer))
            {
                print(hit.transform.name);
                prefab = hit.transform.gameObject;
                Item item = prefab.GetComponent<Item>();
                currentItemName = Utilities.GetItemName(item.eItemType);
                ID = item.ID;
            }
            return; 
         }   
        savingSystem.SavePrefab(currentItemName, ID, prefab.transform.position, prefab.transform.eulerAngles);
        prefab = null;
    }

    void OnDestroy(){
        //Check if the input manager is being initialized to remove all the event subscriptions
        if(inputManager == null)return;
        inputManager.mousePositionEvent -= MovePreview;  
        inputManager.objectRotation -= Rotate;
        inputManager.onMouseLeftClick -= AddItem;
        inputManager.onMouseRightClick -= DeleteItem;
    }
}
