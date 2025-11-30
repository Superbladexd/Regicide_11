using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface GameState 
{
    void doAction(GameContext gamecontext);
}

//游戏开始状态
public class StartState : GameState
{
    public void doAction(GameContext gamecontext)
    {
        CreateDeck createDeck = new CreateDeck(gamecontext.library);

        gamecontext.PlayerDeck = createDeck.CreateNormalDeck().ConvertAll(c => (Card)c);
        gamecontext.TotalBossDeck = createDeck.CreateBossDeck();
        gamecontext.JokerDeck = createDeck.CreateJokerDeck();
        gamecontext.FolderDeck = new List<Card>();

        gamecontext.TotalPlayer = new List<Player>();
        gamecontext.TotalPlayer.Add(new Player("YOU"));
        gamecontext.CurrentPlayerIndex = 0;
        gamecontext.CurrentPlayer = gamecontext.TotalPlayer[gamecontext.CurrentPlayerIndex];
        gamecontext.CurrentPlayer.DrawCards(gamecontext.PlayerDeck,7);

        gamecontext.CurrentBoss = gamecontext.TotalBossDeck[0];
        gamecontext.TotalBossDeck.RemoveAt(0);

        gamecontext.state = gamecontext.gamestate;
    }
}

//游戏中状态，执行每回合操作
public class GamingState : GameState
{
    public void doAction(GameContext gamecontext)
    {
        //玩家回合
        //CardManager中执行完毕

        //Boss回合
        Debug.Log("Boss回合!");
        gamecontext.bossManager.UpdateBossHp();
        if(gamecontext.CurrentBoss.currenthealth <= 0 && gamecontext.TotalBossDeck.Count > 0)
        {
            if(gamecontext.CurrentBoss.currenthealth == 0)
            {
                gamecontext.PlayerDeck.Insert(0,gamecontext.CurrentBoss); //荣誉消灭加入牌堆顶
            }
            gamecontext.CurrentBoss = gamecontext.TotalBossDeck[0];
            gamecontext.TotalBossDeck.RemoveAt(0); //下一个Boss
            gamecontext.bossManager.RefreshBossUI();
            return;
        }
        else if (gamecontext.CurrentBoss.currenthealth <= 0 && gamecontext.TotalBossDeck.Count <= 0)
        {
            gamecontext.state = gamecontext.endstate; //切换为结束状态
            gamecontext.state.doAction(gamecontext);
            return;
        }

        int playerHandTotal = 0;
        foreach (var c in gamecontext.CurrentPlayer.hand)
        {
            playerHandTotal += c.att;
        }

        if (playerHandTotal < gamecontext.CurrentBoss.currentvalue)
        {
            Debug.Log("玩家手牌点数不足，失败！");
            gamecontext.state = gamecontext.endstate;
            gamecontext.state.doAction(gamecontext);
            return;
        }

        // 玩家点数足够，等待玩家选牌丢弃
        gamecontext.cardManager.StartCoroutine(gamecontext.cardManager.PlayerDiscardForBoss(gamecontext.CurrentBoss.currentvalue));

    }
}

//游戏结束状态
public class FinishState : GameState
{
    public void doAction(GameContext gamecontext)
    {
        gamecontext.gameEnded = true;

        if(gamecontext.CurrentBoss.currenthealth <= 0 && gamecontext.TotalBossDeck.Count <= 0)
        {
            Debug.Log("胜利");
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.Log("失败");
            SceneManager.LoadScene("MainScene");
        }

    }
}
