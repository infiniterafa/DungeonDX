using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Inventorry : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] private List<GameObject> _pickedObjects;
    [SerializeField] private List<int> _pickedObjectsUses;
    [SerializeField] private int _gold;

    [SerializeField] private GameObject _tempObj;
    [SerializeField] private Transform _transform;

    [SerializeField] private int _selectedObject;

    public bool tieneLlaveBoss = false;

    [Header("Rupias")]
    [SerializeField] private int rupiasCount = 0;
    [SerializeField] private GameObject gemaPrefab;
    [SerializeField] private Transform gemaSpawnPoint;

    private bool gemaFinalYaInstanciada = false;

    void Start()
    {
        _transform = transform;
        UpdateUIGoldValue();
    }

    public void TakeGold(GameObject gold, int value)
    {
        gold.GetComponent<Gold>().PickUpEfect();
        gold.transform.position = _transform.position;
        gold.transform.parent = _transform;

        _gold += value;
        UpdateUIGoldValue();
    }

    public void UseGold(int amount)
    {
        if (HaveGold(amount))
        {
            _gold -= amount;
            UpdateUIGoldValue();
        }
        else
        {
            Debug.Log("CAN'T AFFORD");
        }
    }

    public bool HaveGold(int cost)
    {
        return _gold >= cost;
    }

    public int GetGold()
    {
        return _gold;
    }

    public void TakePickUp(GameObject pickUp, int uses)
    {
        pickUp.GetComponent<Key>().PickUpEfect();
        pickUp.transform.position = _transform.position;
        pickUp.transform.parent = _transform;

        // Detener movimiento de la llave
        Rigidbody rb = pickUp.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        Collider col = pickUp.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        _pickedObjects.Add(pickUp);
        _pickedObjectsUses.Add(uses);
    }

    public bool HaveNeededItem(GameObject requieredItem)
    {
        for (int i = 0; i < _pickedObjects.Count; i++)
        {
            if (_pickedObjects[i] == requieredItem)
            {
                _selectedObject = i;
                Debug.Log(_selectedObject);
                return true;
            }
        }

        return false;
    }

    public void UseKey()
    {
        Debug.Log(_pickedObjectsUses[_selectedObject]);

        int temp = _pickedObjectsUses[_selectedObject];
        temp -= 1;

        _pickedObjectsUses[_selectedObject] = temp;

        Debug.Log(_pickedObjectsUses[_selectedObject]);

        if (_pickedObjectsUses[_selectedObject] <= 0)
        {
            _pickedObjects.RemoveAt(_selectedObject);
            _pickedObjectsUses.RemoveAt(_selectedObject);
        }
    }

    private void UpdateUIGoldValue()
    {
        if (goldText != null)
        {
            goldText.text = _gold.ToString();
        }
    }

    public void AddRupia()
    {
        rupiasCount++;
        Debug.Log($"Rupias recogidas: {rupiasCount}");

        if (rupiasCount >= 3 && !gemaFinalYaInstanciada)
        {
            if (gemaPrefab != null && gemaSpawnPoint != null)
            {
                Instantiate(gemaPrefab, gemaSpawnPoint.position, Quaternion.identity);
                Debug.Log("¡Has recolectado 3 rupias! Aparece la gema final.");
                gemaFinalYaInstanciada = true;
            }
            else
            {
                Debug.LogWarning("No se asignó el prefab de la gema o el punto de aparición.");
            }

            rupiasCount = 0;
        }
    }
}