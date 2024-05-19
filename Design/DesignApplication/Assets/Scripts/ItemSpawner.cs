using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    private GameObject prefab;
    
    //Temporary should be handle by UI Component.
   // void Awake() => Build();
    
    void Update()
    {
        if(prefab == null) return; 
        MovePreview();
        if (Input.GetMouseButtonDown(0))
            prefab = null;
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if(Input.GetMouseButtonDown(1))
            Rotate();          
    }

    private void MovePreview()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, layer))
        {
            prefab.transform.position = new Vector3(
                        Mathf.Round(hit.point.x),
                        Mathf.Round(hit.point.y) + hit.transform.localScale.y * .5f,
                        Mathf.Round(hit.point.z)
                    );
        }
    }

    public void Build(GameObject template) => prefab = Instantiate(template);
    
    public void Rotate() => prefab.transform.Rotate(new Vector3(0, 90, 0), Space.Self);    
}
