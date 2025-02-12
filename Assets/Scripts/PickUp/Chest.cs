using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _keyBoard;
    [SerializeField] private GameObject _gamePad;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _key;
    [SerializeField] private List<GameObject> _reward;
    [SerializeField] private GameObject _rewardHolder;
    
    public Transform _dropTransform;
    
    private GameObject _player;
    private Player _playerScript;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = FindObjectOfType<Player>();
        _player = _playerScript.gameObject;
        
        _canvas = GetComponentInChildren<Canvas>().gameObject;
        _camera = Camera.main;

        for (int i = 0; i < _rewardHolder.transform.childCount; i++)
        {
            GameObject temp = _rewardHolder.transform.GetChild(i).gameObject;
            _reward.Add(temp);
            temp.SetActive(false);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerScript.chest = true;
            _playerScript.inRange = true;
            _playerScript.canCollect = _playerScript.HaveKey(_key, this.gameObject);
            Debug.Log(_playerScript.canCollect);
        }

        if (!opened)
        {
            _canvas.gameObject.SetActive(true);
        }
        //Debug.Log(other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        _playerScript.chest = false;
        _playerScript.inRange = false;
        _playerScript.canCollect = false;
        _canvas.gameObject.SetActive(false);
    }

    public void DropReward()
    {
        opened = true;
        _canvas.gameObject.SetActive(false);
        
        for (int i = 0; i < _reward.Count; i++)
        {
            _reward[i].transform.position = _dropTransform.position;
            _reward[i].SetActive(true);
        }
    }
}
