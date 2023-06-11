using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class KanjiItem : MonoBehaviour
{
    [SerializeField]
    public Kanji kanji;

    public void GetItem()
    {
        Debug.Log($"Get {kanji.name}");
        Destroy(gameObject);
    }
}
