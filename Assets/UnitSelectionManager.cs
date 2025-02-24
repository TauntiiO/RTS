using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> AllUnits = new List<GameObject>();
    public List<GameObject> SelectedUnits = new List<GameObject>();

    public LayerMask clickable;

    public LayerMask ground;

    public GameObject GroundMarker;
    
    Camera cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Mouse Clicked");
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {

                print(ray.origin + " " + hit.point + " " + hit.collider.gameObject.name);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    print("Selecting Multiple");
                    SelectMultiple(hit.collider.gameObject);
                }
                else
                {
                    print("Selecting Single");
                    SelectByClickig(hit.collider.gameObject);
                }
            }
            else
            {

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && SelectedUnits.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                GroundMarker.transform.position = hit.point;

                GroundMarker.SetActive(false);
                GroundMarker.SetActive(true);
            }
        }

    }

    private void SelectMultiple(GameObject gameObject)
    {
        if (!SelectedUnits.Contains(gameObject))
        {
            SelectedUnits.Add(gameObject);
            EnableUnitMovement(gameObject, true);
            TriggerSelectionIndicator(gameObject, true);
        } else
        {
            EnableUnitMovement(gameObject, false);
            SelectedUnits.Remove(gameObject);
            TriggerSelectionIndicator(gameObject, false);
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in SelectedUnits)
        {
            EnableUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);
        }
        GroundMarker.SetActive(false);
        SelectedUnits.Clear();
    }

    private void SelectByClickig(GameObject unit)
    {
        print("Selecting " + unit.name);
        DeselectAll();

        SelectedUnits.Add(unit);

        TriggerSelectionIndicator(unit, true);

        EnableUnitMovement(unit, true);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisibile)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisibile);
    }
}
