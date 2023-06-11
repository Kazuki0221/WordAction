using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    GameObject _player;
    Player playerScript;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    Text hpText;
    [SerializeField]
    public Text message;

    [SerializeField]
    List<Text> skillList= new List<Text>();

    public bool isClear = false;
    private void Awake()
    {
        if(FindObjectOfType<Player>() == null)
        {
            _player = Instantiate(player, spawnPoint.position, player.transform.rotation);
            virtualCamera.Follow = _player.transform;
            
            playerScript = _player.GetComponent<Player>();
            SkillView();

            hpText.text = $"HPÅF{playerScript.HP}";
        }
        else
        {
            _player = FindObjectOfType<Player>().gameObject;
            virtualCamera.Follow = _player.transform;
            playerScript = _player.GetComponent<Player>();
            SkillView();
            hpText.text = $"HPÅF{playerScript.HP}";
        }
    }

    void Update()
    {
        hpText.text = $"HPÅF{_player.GetComponent<Player>().HP.ToString("f1")}";

        if (isClear)
        {
            StartCoroutine(Clear());
        }

        if(_player.GetComponent<Player>().HP <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        message.text = "GameOver!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Home");
        yield return null;
    }

    public IEnumerator Clear()
    {
        message.text = "GameClear!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Home");
        yield return null;
    }

    public void SkillView()
    {
        for (int i = 0; i < playerScript.Skills.Count; i++)
        {
            skillList[i].text = playerScript.Skills[i].name;
        }
    }
}
