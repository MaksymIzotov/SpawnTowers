using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    #region Singleton Init
    public static DragAndDrop Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private float minX, maxX;
    [SerializeField] private float minY, maxY;

    private bool dragging = false;
    private Transform towerMoving;

    private TowerSpawnAndMerge towerSpawner;

    private void Start()
    {
        towerSpawner = GetComponent<TowerSpawnAndMerge>();
    }

    private void Update()
    {
        DragDrop();
    }

    public bool GetIsDragged(Transform tower)
    {
        return tower == towerMoving;
    }

    private void DragDrop()
    {
        Vector3 v3 = Vector3.zero;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tower")
                {
                    towerMoving = hit.transform;   
                    dragging = true;
                }
            }

            if (towerMoving == null) { return; }

            if (Physics.Raycast(towerMoving.position, towerMoving.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Tile")
                    hit.transform.gameObject.GetComponent<TileColorChanger>().SetActive();
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            RaycastHit hit;
            if (Physics.Raycast(towerMoving.position, towerMoving.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Tile")
                    hit.transform.gameObject.GetComponent<TileColorChanger>().SetActive();
            }

            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position, towerMoving.transform.position));
            towerMoving.position = new Vector3(Mathf.Clamp(Camera.main.ScreenToWorldPoint(v3).x, minX,maxX), towerMoving.position.y, Mathf.Clamp(Camera.main.ScreenToWorldPoint(v3).z, minY, maxY));
        }

        if (dragging && touch.phase == TouchPhase.Stationary)
        {
            RaycastHit hit;
            if (Physics.Raycast(towerMoving.position, towerMoving.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Tile")
                    hit.transform.gameObject.GetComponent<TileColorChanger>().SetActive();
            }
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            RaycastHit hit;
            if (Physics.Raycast(towerMoving.position, towerMoving.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if(hit.transform.tag == "Tile")
                    towerSpawner.CheckTile(hit.transform.name, towerMoving.gameObject);
            }

            dragging = false;
            towerMoving = null;
        }
    }
}
