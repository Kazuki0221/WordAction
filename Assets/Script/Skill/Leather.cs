using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leather : KanjiSkill
{
    public override void Awake()
    {
        base.Awake();
        transform.parent = GameObject.Find("SkillSpawnPoint").transform;
    }

    public override void Action() { }

    public override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
