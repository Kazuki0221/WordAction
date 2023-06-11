using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public static List<Kanji> skills = new List<Kanji>(); //�g�p�\�X�L��
    public GameObject[] skillBtn = new GameObject[4];�@//�X�L���X���b�g

    [SerializeField]
    static GameObject itemList;�@//�A�C�e�����X�g�\���p

    int buttonNum;

    public void Start()
    {
        itemList = FindObjectOfType<ItemList>().gameObject;

        for(int i = 0; i < skills.Count; i++)
        {
            skillBtn[i].GetComponentInChildren<Text>().text = skills[i].name;
        }
    }

    public int ButtonNum  //�{�^���̋�ʗp�v���p�e�B
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
        SceneManager.LoadScene("CombineSkill");
    }

}
