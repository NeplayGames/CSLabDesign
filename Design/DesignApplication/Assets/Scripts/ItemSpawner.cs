using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private LayerMask layer;

    private GameObject prefab;
    
    private Transform mainCameraTransform {get {return Camera.main.transform;}}

    //Temporary should be handle by UI Component.
    void Awake() => Build();
    
    void Update()
    {
        MovePreview();
        if (Input.GetMouseButtonDown(0))
            Build();
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if(Input.GetMouseButtonDown(1))
            Rotate();          
    }

    private void MovePreview()
    {
        if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hit, 30f, layer))
        {
            prefab.transform.position = new Vector3(
                        Mathf.Round(hit.point.x),
                        Mathf.Round(hit.point.y) + hit.transform.localScale.y * .5f,
                        Mathf.Round(hit.point.z)
                    );
        }
    }

    private void Build() => prefab = Instantiate(template);
    
    public void Rotate() => prefab.transform.Rotate(new Vector3(0, 90, 0), Space.Self);    
}
