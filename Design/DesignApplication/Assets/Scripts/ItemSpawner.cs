using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    private GameObject prefab;
    private InputManager inputManager;
    private SaveAndLoadSystem savingSystem;
    private string currentItemName;
    //Temporary should be handle by UI Component.
   // void Awake() => Build();
    
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

    public void Build(GameObject template) 
    {
        currentItemName = template.name;
        prefab = Instantiate(template);
        inputManager.onMouseLeftClick += AddItem;
        inputManager.EnableUI();
    }
    
    public void Rotate() => prefab?.transform.Rotate(new Vector3(0, 90, 0), Space.Self);

    public void SetInputManger(InputManager inputMan, SaveAndLoadSystem savingSystem)
    {
        this.savingSystem = savingSystem;
        this.inputManager = inputMan;
        inputManager.mousePositionEvent += MovePreview;
        inputManager.onMouseRightClick += Rotate;
    }

    private void AddItem()
    {
        savingSystem.SavePrefab(currentItemName, prefab.transform.position, prefab.transform.eulerAngles);
        prefab = null;
        inputManager.onMouseLeftClick -= AddItem;
        inputManager.EnableUI();
    }

    void OnDestroy(){
         inputManager.mousePositionEvent -= MovePreview;  
        inputManager.onMouseRightClick -= Rotate;
         inputManager.onMouseLeftClick -= AddItem;
    }
}
