using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{

    public GameContext gameContext;
    public CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameContext == null)
        {
            gameContext = FindObjectOfType<GameContext>();
        }

        if (cardManager == null)
        {
            cardManager = FindObjectOfType<CardManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //方片抽牌
    public void DrawCards(int point)
    {

        if (gameContext == null)
        {
            Debug.LogError("GameContext 未赋值，无法执行 DrawCards");
            return;
        }

        if (gameContext.CurrentBoss.suit.Equals("♦"))
        {
            return;
        }

        gameContext.CurrentPlayer.DrawCards(gameContext.PlayerDeck,point);
        cardManager.RefreshHandUI();
    }

    //红桃恢复
    public void RecoverCard(int point)
    {
        if (gameContext == null)
        {
            Debug.LogError("GameContext 未赋值，无法执行 RecoverCard");
            return;
        }

        if (gameContext.CurrentBoss.suit.Equals("♥"))
        {
            return;
        }

        List<Card> foldDeck = gameContext.FolderDeck;
        List<Card> playerDeck = gameContext.PlayerDeck;

        if (foldDeck == null || foldDeck.Count == 0)
        {
            Debug.Log("弃牌堆为空，无可恢复卡牌");
            return;
        }

        int takeCount = Mathf.Min(point, foldDeck.Count);

        for (int i = 0; i < takeCount; i++)
        {
            int index = Random.Range(0, foldDeck.Count);
            Card card = foldDeck[index];

            foldDeck.RemoveAt(index);

            playerDeck.Add(card);
        }

        cardManager.RefreshHandUI();
    }

    //黑桃减攻
    public void Defense(int point)
    {
        if (gameContext == null)
        {
            Debug.LogError("GameContext 未赋值，无法执行 Defense");
            return;
        }

        if (gameContext.CurrentBoss.suit.Equals("♠"))
        {
            return;
        }

        gameContext.CurrentBoss.currentvalue = Mathf.Max(0, gameContext.CurrentBoss.currentvalue - point);

    }

    //梅花双倍伤害
    public void DoubleDamage(int point)
    {
        if (gameContext == null)
        {
            Debug.LogError("GameContext 未赋值，无法执行 DoubleDamage");
            return;
        }

        if (gameContext.CurrentBoss.suit.Equals("♣"))
        {
            return;
        }

        gameContext.CurrentBoss.currenthealth -= point;

    }

}
