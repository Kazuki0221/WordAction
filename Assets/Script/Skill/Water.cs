using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : KanjiSkill
{
    [SerializeField]
    float speed;
    Vector3 dir;
    [SerializeField]
    float range;

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, GetFirstPosition()) >= range)
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }

    public override void Attack()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0;
        dir.z = 0;

        GetRigidbody().AddForce(dir * speed, ForceMode.Impulse);
    }

    public override void DestroySkill()
    {
        Destroy(this.gameObject);
    }
}
