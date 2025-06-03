using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("UI")]
    public Image keyIcon;

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
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _deltaPosition;
    private bool _up = true;

    [Header("Audio")]
    public GameObject _model;
    private AudioSource _audioSource;
    public AudioClip _pickUpAudioClip;
    public AudioClip _hoverAudioClip;

    private bool _collected = false;

    void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>()?.gameObject;
        _inventorry = GameObject.FindObjectOfType<Inventorry>();
        _audioSource = gameObject.GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (_model == null)
        {
            Debug.LogWarning("No se asignó el modelo visual (_model) de la llave.");
        }
    }

    void FixedUpdate()
    {
        if (_startPosition == Vector3.zero || _collected) return;

        _curAng += _turnSpeed * Time.deltaTime;
        if (_curAng >= 360f) _curAng -= 360f;

        if (_model != null)
        {
            _model.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, _curAng);
        }

        _deltaPosition = transform.position - _startPosition;

        if (_up)
        {
            if (_deltaPosition.y < _hopTop)
                _currentHop = _hopSpeed * Time.deltaTime;
            else
                _up = false;
        }
        else
        {
            if (_deltaPosition.y > _hopBottom)
                _currentHop = -_hopSpeed * Time.deltaTime;
            else
                _up = true;
        }

        transform.position += new Vector3(0f, _currentHop, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance > 2.0f) return; 

            _inventorry.TakePickUp(this.gameObject, _uses);
        }

        Debug.Log("Tocando capa: " + LayerMask.LayerToName(other.gameObject.layer));

        if (other.gameObject.layer == 31)
        {
            _startPosition = transform.position + new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezeRotationZ;
        }
    }


    IEnumerator TakeKey()
    {
        if (_pickUpAudioClip != null)
        {
            _audioSource.clip = _pickUpAudioClip;
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.volume = 1.0f;
            _audioSource.Play();
        }

        if (keyIcon != null)
        {
            keyIcon.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
    }

    public void PickUpEfect()
    {
        StartCoroutine(TakeKey());

        if (CompareTag("LlaveBoss")) 
        {
            if (_inventorry != null)
            {
                _inventorry.tieneLlaveBoss = true;
                TutorialMensajes tutorial = FindObjectOfType<TutorialMensajes>();
                if (tutorial != null)
                {
                    tutorial.MostrarInstruccionPuerta(); 
                }

                Debug.Log("¡Recogiste la llave del Boss!, Ahora presiona f justo en la PERILLA de la puerta");
            }
        }
    }


}
