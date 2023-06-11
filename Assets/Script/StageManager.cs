using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(player.HP <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Home");
    }
}
