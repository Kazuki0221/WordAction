using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : KanjiSkill
{
    [SerializeField, Header("���ˑ��x")]
    float speed;
    Vector3 dir;
    [SerializeField, Header("���ʔ͈�")]
    float range;

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, GetFirstPosition()) >= range)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Action()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0;
        dir.z = 0;

        GetRigidbody().AddForce(dir * speed, ForceMode.Impulse);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Plant"))
        {
            Destroy(collision.gameObject);
        }
    }
}
