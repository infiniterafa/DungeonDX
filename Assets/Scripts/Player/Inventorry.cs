using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorry : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pickedObjects;
    [SerializeField] private List<int> _pickedObjectsUses;
    [SerializeField] private GameObject _tempObj;
    [SerializeField] private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePickUp(GameObject pickUp, int uses)
    {
        pickUp.SetActive(false);
        pickUp.transform.position = _transform.position;
        pickUp.transform.parent = _transform;

        _pickedObjects.Add(pickUp);
        _pickedObjectsUses.Add(uses);
    }

    public bool HaveNeededItem(GameObject requieredItem)
    {
        for (int i = 0; _pickedObjects.Count > i; i++)
        {
            if (_pickedObjects[i] == requieredItem)
            {
                if (_pickedObjectsUses[i] <= 0)
                {
                    _pickedObjects.RemoveAt(i);
                    _pickedObjectsUses.RemoveAt(i);
                }
                return true;
            }
        }

        return false;
    }
}
