using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KanjiDataBaseManagerTest : MonoBehaviour
{
    [SerializeField]
    private KanjiDataBase kanjiDataBase;  //�����f�[�^�x�[�X

    [SerializeField]
    List<InputField> inputFields= new List<InputField>();

    [SerializeField]
    Text text;

    int buttonNum;
    public int ButtonNum  //�{�^���̋�ʗp�v���p�e�B
    {
        get { return buttonNum; }
        protected set { this.buttonNum = value; }
    }


    public void AddKanjiData(Kanji kanji)
    {
        kanjiDataBase.dataBaseList.Add(kanji);
    }

    public void SetKanji(int number)
    {
        ButtonNum = number;
        ItemList.instance.OpenList();
    }

    /// <summary>
    /// �����̍����𔻕ʂ���֐�
    /// </summary>
    public void Combine()
    {
        string result = string.Empty;
        if (inputFields[0].text != null && inputFields[1].text != null)
        {
            for(int i = 0; i < kanjiDataBase.dataBaseList.Count; i++)
            {
                if (kanjiDataBase.dataBaseList[i].elements.Count <= 1) continue;

                bool[] checks = new bool[kanjiDataBase.dataBaseList[i].elements.Count];
                List<string> list = new List<string>(kanjiDataBase.dataBaseList[i].elements);
                for (int j = 0; j < inputFields.Count; j++)
                {
                    checks[j] = list.Contains(inputFields[j].text);
                    if (checks[j])
                    {
                        list.Remove(inputFields[j].text);
                    }
                }

                if(checks.All(i => i == true))
                {
                    //ItemList.kanjis.Add(kanjiDataBase.dataBaseList[i]);
                    result = kanjiDataBase.dataBaseList[i].name;
                    text.text = result;
                    return;
                }
            }

            text.text = "�������s";
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
}
