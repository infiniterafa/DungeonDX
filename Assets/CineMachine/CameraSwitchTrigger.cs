using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField] private Collider _cameraTrigger;
    [SerializeField] private int _cameraToSwitch;

    public Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTrigger = GetComponent<Collider>();
        _cameraTrigger.isTrigger = true;    
    }

    private void OnTriggerEnter(Collider collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player != null) 
        { 
            CameraManager.instance.CameraSwitch(_cameraToSwitch);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
