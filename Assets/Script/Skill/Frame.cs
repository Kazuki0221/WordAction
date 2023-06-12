using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : KanjiSkill
{
    [SerializeField, Header("”­ŽË‘¬“x")]
    float speed;
    Vector3 dir;
    [SerializeField, Header("Œø‰Ê”ÍˆÍ")]
    float range;

    bool isStrech = true;

    [SerializeField]
    float time = 3f;

    public override void Action()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0f;
        dir.z = 0f;

        Transform tempTransform = gameObject.transform;
        if(transform.localScale.x < range && isStrech)
        {
            transform.localScale += dir * speed * Time.deltaTime;
            tempTransform.localScale = transform.localScale;
        }
        else if(transform.localScale.x >= range && isStrech) 
        {
            transform.localScale = new Vector3(range, 1, 1);
            tempTransform.localScale = transform.localScale;
            isStrech = false;
        }
        else if(!isStrech)
        {
            transform.localScale = tempTransform.localScale;

            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isStrech = false;
        }

        if (collision.gameObject.CompareTag("Plant"))
        {
            Destroy(collision.gameObject);
        }
    }

}
