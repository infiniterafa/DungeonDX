using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField] private Collider _cameraTrigger;
    [SerializeField] private int _cameraToSwitch;
    [SerializeField] private string _staticName;
    [SerializeField] private GameObject _staticCamera;
    [SerializeField] private Transform _cameraPos;

    public Player _player;

    private CameraManager _cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        while (_staticCamera ==  null)
        {
            _staticCamera = GameObject.Find(_staticName);
        }

        while (_cameraTrigger == null) 
        { 
            _cameraTrigger = GetComponent<Collider>();
        }

        while (_cameraManager == null)
        {
            _cameraManager = CameraManager.instance;
        }

        _cameraTrigger.isTrigger = true;    
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision);
        _player = collision.GetComponent<Player>();

        if (_player != null) 
        { 
            CameraManager.instance.CameraSwitch(_cameraToSwitch);

            Debug.Log(CameraManager.instance.name);
            if(_cameraManager.GetCurVC(_cameraToSwitch).name ==  _staticName)
            {
                _staticCamera.transform.position = _cameraPos.position;
                _staticCamera.transform.rotation = _cameraPos.rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
