using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public static ItemList instance; //�V���O���g���p�ϐ�

    [SerializeField]
    public  List<Kanji> kanjis = new List<Kanji>();�@//�莝���̊������X�g
    [SerializeField]
    GameObject kanjibtn; //�����{�^��

    [SerializeField]
    GameObject content; //�{�^���𐶐�����e�I�u�W�F�N�g

    Home homebtn;

    public static bool isUpdate = false;

    /// <summary>
    /// �V���O���g��
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
    /// ItemList�̍쐬
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
    /// �Z�b�g����X�L���̍X�V
    /// </summary>
    /// <param name="k"></param>
    public void UpdateSkill(Kanji k)
    {
        homebtn = FindObjectOfType<Home>();

        //�Z�b�g����Ă���X�L�����Ȃ����X���b�g���Ō�܂Ŗ��܂��Ă��Ȃ��ꍇ
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
        //���ׂẴX���b�g�������Ă���ꍇ
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
