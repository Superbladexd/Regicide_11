using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    // 按钮点击事件
    public void StartGame()
    {
        SceneManager.LoadScene("BattleScene");

    }

    public void QuitGame()
    {
        Debug.Log("退出游戏");
        Application.Quit();
        Application.Quit();
    }

}
