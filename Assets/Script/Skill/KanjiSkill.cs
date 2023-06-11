using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KanjiSkill : MonoBehaviour
{
    Rigidbody rb;
    Vector3 spwanPoint; //skillÇÃê∂ê¨à íu

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spwanPoint = transform.position;
    }

    public virtual void Update()
    {
        Action();
    }

    public abstract void Action();

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public Vector3 GetFirstPosition()
    {
        return spwanPoint; 
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}
