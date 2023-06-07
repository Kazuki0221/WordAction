using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : KanjiSkill
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
            Destroy(this.gameObject);
        }
    }

    public override void Attack()
    {
        dir = FindObjectOfType<Player>().transform.right;
        dir.y = 0;
        dir.z = 0;

        transform.position += new Vector3(dir.x, 0, 0) * speed * Time.deltaTime;
    }

}
