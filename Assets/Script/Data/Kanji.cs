using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
[SerializeField]
public class Kanji : ScriptableObject
{
    public int id;
    public string name;
    public Image img;
    //public float damage;
    //public float range;
    public List<string> elements;
    public GameObject skillObject;
}
