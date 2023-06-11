using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KanjiDataBaseManager : MonoBehaviour
{
    [SerializeField]
    private KanjiDataBase kanjiDataBase;  //漢字データベース

    public List<Kanji> combineList = new List<Kanji>();


    [SerializeField]
    GameObject buttonList;
    [SerializeField]
    GameObject kanjibtn;
    public List<GameObject> kanjibuttons= new List<GameObject>();
    [SerializeField]
    GameObject addMark;
    public List<GameObject> addMarkList = new List<GameObject>();

    [SerializeField]
    Text text;

    int buttonNum;
    public int ButtonNum  //ボタンの区別用プロパティ
    {
        get { return buttonNum; }
        protected set { this.buttonNum = value; }
    }

    int num = 0;

    public bool isClick = false;

    public void AddKanjiData(Kanji kanji)
    {
        kanjiDataBase.dataBaseList.Add(kanji);
    }

    /// <summary>
    /// 漢字の合成を判別する関数
    /// </summary>
    public void Combine()
    {
        string result = string.Empty;
        if (combineList.Count > 1)
        {
            for (int i = 0; i < kanjiDataBase.dataBaseList.Count; i++)
            {
                if (kanjiDataBase.dataBaseList[i].elements.Count <= 1) continue;

                bool[] checks = new bool[kanjiDataBase.dataBaseList[i].elements.Count];
                List<string> list = new List<string>(kanjiDataBase.dataBaseList[i].elements);
                for (int j = 0; j < combineList.Count; j++)
                {
                    checks[j] = list.Contains(combineList[j].name);
                    if (checks[j])
                    {
                        list.Remove(combineList[j].name);
                    }
                }

                if (checks.All(i => i == true))
                {
                    ItemList.kanjis.Add(kanjiDataBase.dataBaseList[i]);
                    result = kanjiDataBase.dataBaseList[i].name;
                    text.text = result;
                    ItemList.isUpdate = true;

                    DestroySlot();             
                    return;
                }
            }

            text.text = "合成失敗";

            DestroySlot();
            return;
        }

    }

    public void ToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void OpenList()
    {
        ItemList.instance.OpenList();
    }

    public void CreateSlot(Kanji _kanji)
    {
        if(combineList.Count > 0 && combineList.Count < 4)
        {
            var addText = Instantiate(addMark);
            addText.transform.parent = buttonList.transform;
            addMarkList.Add(addText);
        }

        var kanjiSlot = Instantiate(kanjibtn);
        kanjiSlot.transform.parent = buttonList.transform;

        RectTransform rtf = kanjiSlot.GetComponent<RectTransform>();
        rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);
        rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250);

        kanjiSlot.GetComponent<Button>().onClick.AddListener(() => ItemList.instance.OpenList());
        kanjiSlot.GetComponent<SetButtonNumber>().number = num;
        kanjiSlot.GetComponent<Button>().onClick.AddListener(() => OnClickButton(kanjiSlot.GetComponent<SetButtonNumber>()));

        kanjibuttons.Add(kanjiSlot);
        if (num < 4)num++;
        kanjiSlot.GetComponentInChildren<Text>().text = _kanji.name;
        combineList.Add(_kanji);
    }

    public void OnClickButton(SetButtonNumber setButtonNumber)
    {
        ButtonNum = setButtonNumber.number;
        isClick = true;
    }

    public void ResetButtonNumber()
    {
        if(num > 0)num--;
        for(int i = 0; i < kanjibuttons.Count; i++)
        {
            if(kanjibuttons[i] != null)
            {
                kanjibuttons[i].GetComponent<SetButtonNumber>().number = i;
            }
           
        }
    }

    void DestroySlot()
    {
        var childCount = buttonList.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            var ui = buttonList.transform.GetChild(i);
            Destroy(ui.gameObject);
        }
        kanjibuttons.Clear();
        addMarkList.Clear();
        combineList.Clear();
    }
}
