using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _keyBoard;
    [SerializeField] private GameObject _gamePad;
    [SerializeField] private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>().gameObject;

        while (!_camera)
        {
            _camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canvas)
        {
            _canvas.transform.rotation =
                Quaternion.LookRotation(_canvas.transform.position-_camera.transform.position);
        }
    }
}
