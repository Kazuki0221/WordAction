using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class KanjiItem : MonoBehaviour
{
    [SerializeField]
    public Kanji kanji;

    public IEnumerator GetItem()
    {
        FindObjectOfType<StageManager>().message.text = $"Get {kanji.name}";
        Destroy(gameObject);
        yield return new WaitForSeconds(2);
        FindObjectOfType<StageManager>().message.text = "";
        yield return null;
    }
}
