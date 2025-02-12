using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody _rb;
    public Camera _mainCamera;
    public Vector3 _cameraFoward;
    public Vector3 _cameraRight;

    public float _speed;
    public float _holdingSpeed;
    public float _rotationSpeed;
    public bool _snapping;
    public bool _rotating = true;

    private float _tempSpeed;

    [Header("HP")]
    public bool _isDead = false;
    public int _hp;
    public int _maxHp = 10;
    public HealthBar _healthBar;


    [Header("Attack")]
    public bool _attacking = false;
    public bool _holding = false;
    public float _holdTime = 1.5f;
    public Collider _attackCollider;
    private float _attackDuration = .5f;
    private float downTime = 0;
    private Sword _sword;

    [Header("Animation")]
    [SerializeField] private Animator _anim;
    Vector3 _rbSpeed;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    public List<AudioClip> _audiosGrunt;
    public List<AudioClip> _audiosWalk;

    void Start()
    {
        while (!_rb)
        {
            _rb = this.gameObject.GetComponent<Rigidbody>();
        }

        while (!_anim)
        {
            _anim = this.gameObject.GetComponent<Animator>();
        }

        while (!_audioSource)
        {
            _audioSource = this.gameObject.GetComponent<AudioSource>();
        }

        while (!_attackCollider)
        {
            _attackCollider = FindObjectOfType<Sword>().GetComponent<Collider>();
        }

        _sword = _attackCollider.GetComponent<Sword>();
        _attackCollider.gameObject.SetActive(false);

        _hp = _maxHp;
        _healthBar.SetMaxHealth(_maxHp);
    }

    void Update()
    {
        if(!_attacking)
        {
            Movement();
            Attack();
        }

        if(_hp <= 0)
        {
            _healthBar.SetHealth(0);
            _anim.SetTrigger("Dead");
        }
    }

    void Movement()
    {
        if (_mainCamera == null)
        {
            return;
        }

        WASD();

        if(_rotating)
            Rotate();

        Animate();
    }

    private void WASD()
    {
        
        if(!_holding)
        {
            _tempSpeed = _speed;
        }
        else
        {
            _tempSpeed = _holdingSpeed;
        }

        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");


        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = horInput * _cameraRight;
        Vector3 forwardRelative = verInput * _cameraFoward;

        Vector3 moveDir = (forwardRelative + rightRelative).normalized * _tempSpeed;
        _rb.velocity = new Vector3(moveDir.x, _rb.velocity.y, moveDir.z);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rotating = false;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _rotating = true;
        }

        if(_rbSpeed != Vector3.zero && !_audioSource.isPlaying)
        {
            if (_rbSpeed.y != 0)
            {
                //Debug.Log("FALLING");
            }
            else
            {
                var i = Random.Range(0, _audiosWalk.Count);
                _audioSource.clip = _audiosWalk[i];
                _audioSource.Play();
            }
        }
    }

    private void Rotate()
    {
        if(_rbSpeed != Vector3.zero)
        {
            if(_snapping)
            {
                transform.forward = new Vector3(_rbSpeed.x, 0 , _rbSpeed.z);
            }
            else
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(_rbSpeed.x, 0, _rbSpeed.z), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            downTime += Time.deltaTime;
            _holding = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            //Debug.Log(downTime);
            if(downTime < _holdTime)
            {
                _anim.SetTrigger("Attack");
                _anim.SetFloat("AttackNum", Random.Range(1, 3));
                _sword.SetPosition(0);
                StartCoroutine(TurnAttackCollider(_attackDuration));
            }
            else
            {
                _anim.SetTrigger("Attack");
                _anim.SetFloat("AttackNum", 4);
                _sword.SetPosition(1);
                StartCoroutine(TurnAttackCollider(_attackDuration));
            }

            var i = Random.Range(0,_audiosGrunt.Count);
            _audioSource.clip = _audiosGrunt[i];
            _audioSource.Play();

            downTime = 0;
            _holding = false;
        }
    }

    private IEnumerator TurnAttackCollider(float attackDuration)
    {
        _attacking = true;
        _attackCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        _attacking = false;
        _attackCollider.gameObject.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        _hp -= dmg;
        _healthBar.SetHealth(_hp);
    }

    private void Animate()
    {
        _rbSpeed = (_rb.velocity).normalized;

        if (_rbSpeed != Vector3.zero)
        {
            _anim.SetBool("Moving", true);
        }
        else
        {
            _anim.SetBool("Moving", false);
        }

        _anim.SetFloat("Ver", Vector3.Dot(_rbSpeed,transform.forward));
        _anim.SetFloat("Hor", Vector3.Dot(_rbSpeed,transform.right));
    }
}
