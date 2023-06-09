using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
[SerializeField]
public class Kanji : ScriptableObject
{
    public int id; //�����ԍ�
    public string name;�@//����
    public Image img;
    //public float damage;
    //public float range;
    public List<string> elements;�@//�\������Ă��銿��
    public GameObject skillObject; //�X�L���p�I�u�W�F�N�g
}
