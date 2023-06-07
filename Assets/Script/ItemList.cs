using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public static ItemList instance;

    [SerializeField]
    List<Kanji> kanjis = new List<Kanji>();
    [SerializeField]
    GameObject kanjibtn;

    [SerializeField]
    GameObject content;

    Home homebtn;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        CreateList();
    }

    void CreateList()
    {
        if(kanjis != null)
        {
            foreach (var k in kanjis)
            {
                var btn = Instantiate(kanjibtn);
                btn.transform.parent = content.gameObject.transform;
                btn.GetComponent<Button>().onClick.AddListener(() => UpdateSkill(k));
                btn.GetComponentInChildren<Text>().text = k.name;
            }
        }
    }

    public void UpdateSkill(Kanji k)
    {
        homebtn = FindObjectOfType<Home>();
        if (Home.skills == null || Home.skills.Count < homebtn.ButtonNum + 1)
        {
            if (!Home.skills.Contains(k))
            {
                Home.skills.Add(k);
                homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = k.name;
            }
        }
        else
        {
            Home.skills[homebtn.ButtonNum] = k;
            homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = k.name;

        }
        this.gameObject.SetActive(false);
    }
}
