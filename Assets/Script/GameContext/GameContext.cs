using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameContext : MonoBehaviour
{
    public List<BossCard> TotalBossDeck; //boss
    public List<Card> PlayerDeck; //抽牌
    public List<Card> FolderDeck; //弃牌
    public List<JokerCard> JokerDeck; //小丑牌

    public List<Player> TotalPlayer; //所有玩家（如果需要扩展联机内容）
    public Player CurrentPlayer; //当前玩家
    public int CurrentPlayerIndex; //当前玩家标号
    public BossCard CurrentBoss; //当前Boss

    public int roundNumber;  //当前回合数
    public bool gameEnded; //判断游戏是否结束

    //其他脚本
    public CardManager cardManager;
    public CreateCards library;
    public BossManager bossManager;

    public GameState startstate = new StartState();
    public GameState gamestate = new GamingState();
    public GameState endstate = new FinishState();
    public GameState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (cardManager == null)
            cardManager = FindObjectOfType<CardManager>();

        if (library == null)
            library = FindObjectOfType<CreateCards>();

        if (bossManager == null)
            bossManager = FindObjectOfType<BossManager>();

        if (library != null)
        {
            state = startstate;
            state.doAction(this);
            cardManager.gameContext = this;

            Debug.Log(CurrentPlayerIndex);
            Debug.Log(CurrentPlayer.playername);
            Debug.Log(CurrentPlayer.hand.Count);
            Debug.Log(TotalPlayer.Count);
            Debug.Log(CurrentBoss.suit + CurrentBoss.point);
            // 刷新 UI
            cardManager.RefreshHandUI();

        }
        else
        {
            Debug.LogError("CreateCards library 未找到！");
        }

        // 初始化 BossManager
        bossManager.Init(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
