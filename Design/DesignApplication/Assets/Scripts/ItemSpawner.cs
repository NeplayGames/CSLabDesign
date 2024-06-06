using System;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private LayerMask wallLayer;
    private Item prefab;
    private InputManager inputManager;
    public event Action NewItemSelected;
    private SaveAndLoadSystem savingSystem;
    private string currentItemName;
    private string ID;   
    private bool pressX, pressY, pressZ; 
    private void MovePreview(Vector3 mousePosition)
    {
        if(prefab == null) return; 
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30f, floorLayer))
        {
            prefab.transform.position = GetFloorPosition(hit);
        }
        else if (Physics.Raycast(ray, out hit, 30f, wallLayer))
        {
            prefab.transform.position = hit.point;
        }
    }

    private Vector3 GetFloorPosition(RaycastHit hit)
    {
        return new Vector3(
                    Mathf.Round(hit.point.x),
                    Mathf.Round(hit.point.y) + hit.transform.localScale.y * .5f,
                    Mathf.Round(hit.point.z)
                );
    }


  
    public void Build(Item template) 
    {
        if(prefab){
            Destroy(prefab.gameObject);
        }
        currentItemName = template.name;
        template.Do(x => {
            x.SetID();
            prefab = Instantiate(template.gameObject).GetComponent<Item>();
            ID = x.ID;
        }
        );
        NewItemSelected?.Invoke();
    }
    
    public void Rotate() => prefab?.transform.Rotate(new Vector3(0, 90, 0), Space.Self);

    public void SetInputManger(InputManager inputMan, SaveAndLoadSystem savingSystem)
    {
        this.savingSystem = savingSystem;
        this.inputManager = inputMan;
        inputManager.MousePositionEvent += MovePreview;
        inputManager.OnMouseLeftClick += EditOrAddItem;
        inputManager.OnMouseRightClick += DeleteItem;
        inputManager.ObjectRotation += Rotate;
        inputManager.MouseScrollChange += MouseScrollChange;
        inputManager.OnXClick += OnXClick;
        inputManager.OnYClick += OnYClick;
        inputManager.OnZClick += OnZClick;
    }

    private void OnZClick(bool obj)
    {
        pressX = obj;
    }

    private void OnYClick(bool obj)
    {
        pressY = obj;
    }

    private void OnXClick(bool obj)
    {
        pressZ = obj;
    }

    private void MouseScrollChange(float obj)
    {
        if(prefab!=null && !pressX && !pressY && !pressZ)
            prefab.ChangeSize(obj, obj, obj);
        else
            prefab.ChangeSize(pressX ? obj : 0, pressY ? obj : 0, pressZ ? obj : 0); 
    }

    private void DeleteItem()
    {     
        GameObject itemOb = null;   
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, itemLayer))
        {
            itemOb = hit.transform.gameObject;
        }
        if (itemOb == null)
            return;  
        prefab = itemOb.GetComponent<Item>();
        savingSystem.RemoveItem(prefab.ID);
        Destroy(prefab.gameObject);
        prefab = null;
    }

    private void EditOrAddItem()
    {
         if(prefab == null)
         {
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30f, itemLayer))
            {
                print(hit.transform.name);
                prefab = hit.transform.GetComponent<Item>();
                currentItemName = prefab.itemName;
                ID = prefab.ID;
            }
            return; 
         }   
        savingSystem.SavePrefab(currentItemName, ID, prefab.transform.position, prefab.transform.eulerAngles, prefab.transform.localScale);
        prefab = null;
    }

    void OnDestroy(){
        //Check if the input manager is being initialized to remove all the event subscriptions
        if(inputManager == null)return;
        inputManager.MousePositionEvent -= MovePreview;  
        inputManager.ObjectRotation -= Rotate;
        inputManager.OnMouseLeftClick -= EditOrAddItem;
        inputManager.OnMouseRightClick -= DeleteItem;
         inputManager.MouseScrollChange -= MouseScrollChange;
         inputManager.OnXClick -= OnXClick;
        inputManager.OnYClick -= OnYClick;
        inputManager.OnZClick -= OnZClick;
    }
}
