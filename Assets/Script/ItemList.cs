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
    GameObject list;

    [SerializeField]
    GameObject closeButton;

    [SerializeField]
    GameObject content; //ボタンを生成する親オブジェクト

    [SerializeField]
    Scrollbar scrollVertical = default;

    public static bool isUpdate = false;

    /// <summary>
    /// シングルトン
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CreateList();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(list.GetComponent<Image>().color == Color.white)
        {
            float val = Input.GetAxisRaw("Mouse ScrollWheel");
            float sValue = scrollVertical.value;

            if(val > 0)
            {
                if(sValue < 1)
                {
                    sValue += 0.5f;
                }
                else if(sValue >= 1)
                {
                    sValue = 1;
                }
            }
            else if(val < 0)
            {
                if(sValue > 0)
                {
                    sValue -= 0.5f;
                }
                else if(sValue <= 0)
                {
                    sValue = 0;
                }
            }

            scrollVertical.value = sValue;
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

    /// <summary>
    /// リストを開く処理
    /// </summary>
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

    public void CloseList()
    {
        ListActiveOrDisActive(false, Color.clear);
    }

    /// <summary>
    /// セットするスキルの更新
    /// </summary>
    /// <param name="k"></param>
    public void UpdateSkill(Kanji k)
    {
        var homebtn = FindObjectOfType<Home>();

        var kanjiDataManager = FindObjectOfType<KanjiDataBaseManager>();

        if(homebtn)
        {
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
                if (!Home.skills.Contains(k))
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

        /*
        if (kanjiDataManager)
        {
            //セットされているスキルがないかスロットが最後まで埋まっていない場合
            if (Home.skills == null || Home.skills.Count < kanjiDataManager.ButtonNum + 1)
            {
                if (!Home.skills.Contains(k))
                {
                    Home.skills.Add(k);
                    kanjiDataManager.skillBtn[kanjiDataManager.ButtonNum].GetComponentInChildren<Text>().text = k.name;
                }
                else
                {
                    int index = Home.skills.IndexOf(k);
                    Home.skills.Add(k);

                    kanjiDataManager.skillBtn[kanjiDataManager.ButtonNum].GetComponentInChildren<Text>().text = k.name;

                    Home.skills[index] = null;
                    kanjiDataManager.skillBtn[index].GetComponentInChildren<Text>().text = "";
                }
            }
            //すべてのスロットが入っている場合
            else
            {
                if (!Home.skills.Contains(k))
                {
                    Home.skills[kanjiDataManager.ButtonNum] = k;
                    kanjiDataManager.skillBtn[kanjiDataManager.ButtonNum].GetComponentInChildren<Text>().text = k.name;
                }
                else
                {
                    int index = Home.skills.IndexOf(k);

                    var tempSkill = Home.skills[index];
                    var tempText = kanjiDataManager.skillBtn[kanjiDataManager.ButtonNum].GetComponentInChildren<Text>().text;

                    Home.skills[index] = Home.skills[kanjiDataManager.ButtonNum];
                    kanjiDataManager.skillBtn[index].GetComponentInChildren<Text>().text = tempText;

                    Home.skills[kanjiDataManager.ButtonNum] = tempSkill;
                    kanjiDataManager.skillBtn[kanjiDataManager.ButtonNum].GetComponentInChildren<Text>().text = tempSkill.name;
                }
            }
            ListActiveOrDisActive(false, Color.clear);
        }
        */

    }

    /// <summary>
    /// リストの表示/非表示
    /// </summary>
    /// <param name="active"></param>
    /// <param name="color"></param>
    void ListActiveOrDisActive(bool active, Color color)
    {
        var listImage = list.GetComponent<Image>();
        listImage.color = color;
        listImage.raycastTarget = active;

        int childCount = list.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject childObject = list.transform.GetChild(i).gameObject;
            if (i == 1)
            {
                var scrollImage = childObject.GetComponent<Image>();
                scrollImage.color = color;
                scrollImage.raycastTarget = active;
                childObject.transform.GetChild(0).gameObject.SetActive(active);
                scrollVertical.value = 1;
            }
            else
            {
                childObject.SetActive(active);
            }
        }

        closeButton.SetActive(active);
    }
}
