using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KanjiSkill : MonoBehaviour
{
    Rigidbody rb;
    Vector3 spwanPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spwanPoint = transform.position;
    }

    public virtual void Update()
    {
        Attack();
    }

    public abstract void Attack();

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public Vector3 GetFirstPosition()
    {
        return spwanPoint; 
    }

    public abstract void DestroySkill();

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            DestroySkill();
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}
