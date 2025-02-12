using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("INVENTORY PARAMETERS")]
    private GameObject _player;
    private Inventorry _inventorry;
    [SerializeField] private int _uses;


    [Header("TURN EFFECTS")]
    public float _turnSpeed;
    private float _curAng = 0.0f;

    [Header("HOP EFFECTS")]
    public float _hopSpeed;
    public float _hopTop;
    public float _hopBottom;
    private float _currentHop = 0.0f;
    private Vector3 _startPosition;
    private bool _up = true;

    // Start is called before the first frame update
    void Start()
    {
        while (!_player)
        {
            _player = GameObject.FindObjectOfType<Player>().gameObject;
        }

        Debug.Log(_player);

        while (!_inventorry)
        {
            _inventorry = GameObject.FindObjectOfType<Inventorry>();
        }

        _startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        //ROTATION ANIMATION
        _curAng += _turnSpeed * Time.deltaTime;
        if (_curAng >= 360)
        {
            _curAng -= 360.0f;
        }
        gameObject.transform.rotation = Quaternion.Euler(-90.0f,0.0f,_curAng);


        //HOP ANIMATION
        if (_up)
        {
            if (_currentHop < _hopTop)
            {
                _currentHop += _hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = false;
            }
        }
        else
        {
            if (_currentHop > _hopBottom)
            {
                _currentHop -= _hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = true;
            }
        }

        gameObject.transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z + _currentHop);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _player)
        {
            _inventorry.TakePickUp(this.gameObject, _uses);
        }

        Debug.Log(other.gameObject.name);
    }

    void Collect()
    {
    }
}
