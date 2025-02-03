using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Camera _mainCamera;
    public Vector3 _cameraFoward;
    public Vector3 _cameraRight;

    public float _speed;
    public int hp = 10;

    private float attackDuration = .75f;
    public Collider attackCollider;

    void Start()
    {
        while (!rb)
        {
            rb = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        if (_mainCamera == null)
        {
            return;
        }

        float horInput = Input.GetAxisRaw("Horizontal") * _speed;
        float verInput = Input.GetAxisRaw("Vertical") * _speed;

        
        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = horInput * _cameraRight;
        Vector3 forwardRelative = verInput * _cameraFoward;

        Vector3 moveDir = forwardRelative + rightRelative;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y , moveDir.z);

        //this.transform.position += new Vector3(moveDir.x,0,moveDir.z); POS METH
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(TurnAttackCollider(attackDuration));
        }
    }

    private IEnumerator TurnAttackCollider(float _attackDuration)
    {
        attackCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(_attackDuration);
        attackCollider.gameObject.SetActive(false);
    }

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;
    }
}
