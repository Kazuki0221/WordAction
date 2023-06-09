using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public static List<Kanji> skills = new List<Kanji>(); //使用可能スキル
    public GameObject[] skillBtn = new GameObject[4];　//スキルスロット

    [SerializeField]
    GameObject itemList;　//アイテムリスト表示用

    int buttonNum;

    public int ButtonNum  //ボタンの区別用プロパティ
    {
        get { return buttonNum; } 
        protected set { this.buttonNum = value; } 
    }

    public void UpdateSkill(int number)
    {
        ButtonNum = number;
        itemList.GetComponent<ItemList>().OpenList();
    }

    public void IsStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Combine()
    {
        SceneManager.LoadScene("KanjiTest");
    }

}
