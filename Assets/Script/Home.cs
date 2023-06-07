using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public static List<Kanji> skills = new List<Kanji>();
    public GameObject[] skillBtn = new GameObject[4];

    [SerializeField]
    GameObject itemList;

    int buttonNum;

    public int ButtonNum 
    {
        get { return buttonNum; } 
        protected set { this.buttonNum = value; } 
    }

    private void Start()
    {
        itemList.SetActive(false);
    }

    public void UpdateSkill(int number)
    {
        ButtonNum = number;
        itemList.SetActive(true);
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
