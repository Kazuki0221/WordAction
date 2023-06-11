using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    [SerializeField, Header("HP")]
    float hp = 5f;

    public float HP
    {
        get { return hp; }
        protected set { hp = value; }
    }
    [SerializeField,Header("移動速度")]
    float speed;
    [SerializeField,Header("ジャンプ力")]
    float jumpPower;
    Rigidbody _rb;

    [SerializeField,Header("スキルの生成位置")]
    Transform skillSpawnPoint;

    [SerializeField,Header("保有スキル")]
    List<Kanji> skills;

    public List<Kanji> Skills
    {
        get { return skills; }
        protected set { skills = value; }
    }

    KanjiItem item;
    bool isGetSkill = false;

    public bool GetSkill
    {
        get { return isGetSkill; }
        protected set { isGetSkill = value; }
    }

    bool isGoround = true; //地面判定用変数

    StageManager stageManager;
    void Awake()
    {
        _rb= GetComponent<Rigidbody>();
        stageManager = FindObjectOfType<StageManager>();
        skills = new List<Kanji>(Home.skills);
    }

    void Update()
    {
        if (!stageManager.isClear)
        {
            Move();

            if (isGetSkill)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    ItemList.kanjis.Add(item.kanji);
                    if (!Home.skills.Contains(item.kanji) && Home.skills.Count < 4)
                    {
                        skills.Add(item.kanji);
                        Home.skills = skills;
                    }
                    stageManager.SkillView();
                    StartCoroutine(item.GetItem());
                    isGetSkill = false;
                    ItemList.isUpdate = true;
                }
            }

            UseSkill();
        }

    }

    /// <summary>
    /// 移動関連
    /// </summary>
    private void Move()
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

    /// <summary>
    /// スキル判別
    /// </summary>
    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills[0] != null)
        {
            Instantiate(skills[0].skillObject, skillSpawnPoint.position, skills[0].skillObject.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && skills[1] != null)
        {
            Instantiate(skills[1].skillObject, skillSpawnPoint.position, skills[1].skillObject.transform.rotation);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && skills[2] != null)
        {
            Instantiate(skills[2].skillObject, skillSpawnPoint.position, skills[2].skillObject.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && skills[3] != null)
        {
            Instantiate(skills[3].skillObject, skillSpawnPoint.position, skills[3].skillObject.transform.rotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isGoround = true;

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(HP > 0)
            {
                HP -= 0.5f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Fire"))
        {
            HP -= 0.1f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGoround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Goal"))
        {
            stageManager.isClear = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<KanjiItem>())
        {
            item = other.gameObject.GetComponent<KanjiItem>();
            isGetSkill= true;
        }
    }
}
