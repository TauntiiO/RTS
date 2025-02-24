using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private void Start()
    {
        UnitSelectionManager.Instance.AllUnits.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.AllUnits.Remove(this.gameObject);
        UnitSelectionManager.Instance.SelectedUnits.Remove(this.gameObject);
    }
}
