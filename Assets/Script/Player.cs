using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpPower;
    Rigidbody _rb;

    bool isGoround = true;
    void Start()
    {
        _rb= GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = new Vector3(h, 0, 0);
        float verticalVelocity = _rb.velocity.y;
        _rb.velocity = dir.normalized * speed + Vector3.up * verticalVelocity;

        if (_rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(_rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        if (Input.GetAxis("Jump") > 0 && isGoround)
        {
            _rb.AddForce(Vector3.up * jumpPower * 3, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGoround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGoround = false;
    }
}
