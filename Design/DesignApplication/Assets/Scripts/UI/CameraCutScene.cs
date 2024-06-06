using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCutScene : MonoBehaviour
{
     private bool running = false;
    public Vector3 cameraSpeeds;
    public CutSceneItems[] startPos;
    private Transform target;
    public float distanceToTravel; 

    private float covered;
    private int index;
    public void SetCamera(bool start)
    {
        running = start;
        if(!running)return;
        covered = 0;
        index = -1;
        SetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
            Movement();
    }

  
    private void Movement()
    {
        Vector3 previousPosition = this.transform.position;
        transform.Translate(cameraSpeeds*Time.deltaTime, Space.World);
        transform.LookAt(target);
        covered += Vector3.Distance(previousPosition, this.transform.position);
        if(Vector3.Distance(target.position, this.transform.position) <= 3f)
        {
            covered = distanceToTravel * 2;
        }
        if (covered >= distanceToTravel)
        {
            transform.position = new Vector3(target.position.x + Random.Range(-10, 10), target.transform.position.y + Random.Range(0, 10), target.transform.position.z + Random.Range(-10, 10));
            covered = 0;
            SetCameraPosition();
        }
    }

    private void SetCameraPosition()
    {  
        index += 1;
        if (index >= startPos.Length)
            index = 0;
        this.transform.position =  startPos[index].GetStartPoint;
        this.target =  startPos[index].target;
    }
}

[System.Serializable]

public class CutSceneItems{
    [SerializeField] private Transform startPoint;
    public Transform target;

    public Vector3 GetStartPoint{
        get{ return startPoint.position; }
    }
}