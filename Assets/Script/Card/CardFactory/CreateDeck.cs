using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDeck
{
    public List<NormalCard> normalCards = new List<NormalCard>();
    public List<BossCard> bossCards = new List<BossCard>();
    public List<JokerCard> jokerCards = new List<JokerCard>();

    public CreateCards createcards;

    public CreateDeck(CreateCards library)
    {
        createcards = library; // 引用场景里的 CreateCards 实例
    }

    public List<NormalCard> CreateNormalDeck()
    {
        foreach(var card in createcards.normalcardList)
        {
            normalCards.Add(new NormalCard((NormalCard)card));
        }
        Shuffle(normalCards);
        return normalCards;
    }

    public List<BossCard> CreateBossDeck()
    {
        List<BossCard> Jdeck = new List<BossCard>();
        List<BossCard> Kdeck = new List<BossCard>();
        List<BossCard> Qdeck = new List<BossCard>();
        foreach (var card in createcards.bosscardList)
        {
            if (card.point.Equals("J"))
            {
                Jdeck.Add(new BossCard((BossCard)card));
            }
            else if (card.point.Equals("Q"))
            {
                Qdeck.Add(new BossCard((BossCard)card));
            }
            else if (card.point.Equals("K"))
            {
                Kdeck.Add(new BossCard((BossCard)card));
            }
        }
        Shuffle(Jdeck);
        Shuffle(Qdeck);
        Shuffle(Kdeck);
        bossCards.AddRange(Jdeck);
        bossCards.AddRange(Qdeck);
        bossCards.AddRange(Kdeck);
        return bossCards;
    }

    public List<JokerCard> CreateJokerDeck()
    {
        foreach (var card in createcards.jokercardList)
        {
            jokerCards.Add(new JokerCard((JokerCard)card));
        }
        Shuffle(jokerCards);
        return jokerCards;
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

}
