using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public List<Card> hand = new List<Card>();
    public string playername;

    public Player(string playername)
    {
        this.playername = playername;
    }

    //摸牌
    public void AddHand(Card card)
    {
        hand.Add(card);
    }

    // 按点数排序
    public void SortByRank()
    {
        // 定义点数的顺序
        List<string> rankOrder = new List<string>()
        {"A","2","3","4","5","6","7","8","9","10","J","Q","K"};

        hand = hand.OrderBy(card => rankOrder.IndexOf(card.point)).ToList();
    }

    // 按花色分类（♠, ♥, ♣, ♦ 的顺序）
    public void SortBySuit()
    {
        Dictionary<string, int> suitOrder = new Dictionary<string, int>()
        {
            {"♠", 1},
            {"♥", 2},
            {"♣", 3},
            {"♦", 4}
        };
        var rankOrder = new List<string> { "A" ,"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"}; //排列顺序

        hand = hand.OrderBy(card => suitOrder[card.suit])
                   .ThenBy(card => rankOrder.IndexOf(card.point)) // 花色内再按点数
                   .ToList();
    }

    public void DrawCards(List<Card> deck,int count)
    {
        for (int i = 0; i < count && deck.Count > 0 && hand.Count < 8; i++)
        {
            Card drawn = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawn);
        }
    }



}
