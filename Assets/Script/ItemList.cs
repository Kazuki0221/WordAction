using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public static ItemList instance; //シングルトン用変数

    [SerializeField]
    public  List<Kanji> kanjis = new List<Kanji>();　//手持ちの漢字リスト
    [SerializeField]
    GameObject kanjibtn; //漢字ボタン

    [SerializeField]
    GameObject content; //ボタンを生成する親オブジェクト

    Home homebtn;

    public static bool isUpdate = false;

    /// <summary>
    /// シングルトン
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            CreateList();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ItemListの作成
    /// </summary>
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

        ListActiveOrDisActive(false, Color.clear);
    }

    public void OpenList()
    {

        if (kanjis != null && isUpdate)
        {
            int btnCount = content.transform.childCount;
            for (int i = 0; i < btnCount; i++)
            {
                GameObject childObject = content.transform.GetChild(i).gameObject;
                Destroy(childObject);
            }

            foreach (var k in kanjis)
            {
                var btn = Instantiate(kanjibtn);
                btn.transform.parent = content.gameObject.transform;
                btn.GetComponent<Button>().onClick.AddListener(() => UpdateSkill(k));
                btn.GetComponentInChildren<Text>().text = k.name;
            }
            isUpdate = false;
        }

        ListActiveOrDisActive(true, Color.white);
    }

    /// <summary>
    /// セットするスキルの更新
    /// </summary>
    /// <param name="k"></param>
    public void UpdateSkill(Kanji k)
    {
        homebtn = FindObjectOfType<Home>();

        //セットされているスキルがないかスロットが最後まで埋まっていない場合
        if (Home.skills == null || Home.skills.Count < homebtn.ButtonNum + 1)
        {
            if (!Home.skills.Contains(k))
            {
                Home.skills.Add(k);
                homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = k.name;
            }
            else
            {
                int index = Home.skills.IndexOf(k);
                Home.skills.Add(k);

                homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = k.name;

                Home.skills[index] = null;
                homebtn.skillBtn[index].GetComponentInChildren<Text>().text = "";
            }
        }
        //すべてのスロットが入っている場合
        else
        {
            if(!Home.skills.Contains(k))
            {
                Home.skills[homebtn.ButtonNum] = k;
                homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = k.name;
            }
            else
            {
                int index = Home.skills.IndexOf(k);

                var tempSkill = Home.skills[index];
                var tempText = homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text;

                Home.skills[index] = Home.skills[homebtn.ButtonNum];
                homebtn.skillBtn[index].GetComponentInChildren<Text>().text = tempText;

                Home.skills[homebtn.ButtonNum] = tempSkill;
                homebtn.skillBtn[homebtn.ButtonNum].GetComponentInChildren<Text>().text = tempSkill.name;
            }
        }

        ListActiveOrDisActive(false, Color.clear);
    }

    void ListActiveOrDisActive(bool active, Color color)
    {
        var listImage = gameObject.GetComponent<Image>();
        listImage.color = color;
        listImage.raycastTarget = active;

        int childCount = gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            GameObject childObject = gameObject.transform.GetChild(i).gameObject;
            childObject.SetActive(active);
        }
    }
}
