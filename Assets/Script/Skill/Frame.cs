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
    Transform tempTransform;

    public override void Action()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0f;
        dir.z = 0f;

        if(transform.localScale.x < range && isStrech)
        {
            transform.localScale += dir * speed * Time.deltaTime;
            tempTransform.localScale = transform.localScale;

        }
        else if(transform.localScale.x >= range && isStrech) 
        {
            transform.localScale = new Vector3(range, 1, 1);
        }
        else if(!isStrech)
        {
            transform.localScale = tempTransform.localScale;
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            isStrech = false;
        }

        if (collision.gameObject.CompareTag("PLant"))
        {
            Destroy(collision.gameObject);
        }
    }

}
