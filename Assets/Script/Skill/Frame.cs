using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : KanjiSkill
{
    [SerializeField, Header("���ˑ��x")]
    float speed;
    Vector3 dir;
    [SerializeField, Header("���ʔ͈�")]
    float range;

    public override void Attack()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0f;
        dir.z = 0f;

        if(transform.localScale.x < range)
        {
            transform.localScale += dir * speed * Time.deltaTime;
        }
        else
        {
            transform.localScale = new Vector3(range, 1, 1);
            Debug.Log(dir);
        }
    }
}
