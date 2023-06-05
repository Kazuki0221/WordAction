using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class KanjiDataBase : ScriptableObject
{
    public List<Kanji> dataBaseList = new List<Kanji>();
}
