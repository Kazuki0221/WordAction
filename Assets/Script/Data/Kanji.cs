using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
[SerializeField]
public class Kanji : ScriptableObject
{
    public int id; //漢字番号
    public string name;　//漢字
    public Image img;
    //public float damage;
    //public float range;
    public List<string> elements;　//構成されている漢字
    public GameObject skillObject; //スキル用オブジェクト
}
