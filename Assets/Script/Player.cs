using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField,Header("�ړ����x")]
    float speed;
    [SerializeField,Header("�W�����v��")]
    float jumpPower;
    Rigidbody _rb;

    [SerializeField,Header("�X�L���̐����ʒu")]
    Transform skillSpawnPoint;

    [SerializeField,Header("�ۗL�X�L��")]
    List<Kanji> skills;

    bool isGoround = true; //�n�ʔ���p�ϐ�        
    void Start()
    {
        _rb= GetComponent<Rigidbody>();
        skills = new List<Kanji>(Home.skills);
    }

    void Update()
    {
        //�X�L������
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


    /// <summary>
    /// �ړ��֘A
    /// </summary>
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
