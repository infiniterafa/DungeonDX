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
    private Vector3 _deltaPosition;
    private bool _up = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().gameObject;
        _inventorry = GameObject.FindObjectOfType<Inventorry>();
        _startPosition = transform.position;
    }

    void FixedUpdate()
    {
        _deltaPosition = transform.position - _startPosition;
        
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
            if (_deltaPosition.y < _hopTop)
            {
                _currentHop = _hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = false;
            }
        }
        else
        {
            if (_deltaPosition.y > _hopBottom)
            {
                _currentHop = -_hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = true;
            }
        }

        gameObject.transform.position += new Vector3(0.0f,_currentHop, 0.0f); 
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _player)
        {
            _inventorry.TakePickUp(this.gameObject, _uses);
        }

        //Debug.Log(other.gameObject.name);
    }

    void Collect()
    {
    }
}
