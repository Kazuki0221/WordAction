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

    [SerializeField]
    Transform skillSpawnPoint;

    [SerializeField]
    List<Kanji> skills = new List<Kanji>();

    bool isGoround = true;
    void Start()
    {
        _rb= GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(skills[0].skillObject, skillSpawnPoint.position, skills[0].skillObject.transform.rotation);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(skills[1].skillObject, skillSpawnPoint.position, skills[1].skillObject.transform.rotation);

        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(skills[2].skillObject, skillSpawnPoint.position, skills[2].skillObject.transform.rotation);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(skills[3].skillObject, skillSpawnPoint.position, skills[3].skillObject.transform.rotation);
        }

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


        if (Input.GetButtonDown("Jump") && isGoround)
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
