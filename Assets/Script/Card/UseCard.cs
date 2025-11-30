using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCard : MonoBehaviour
{

    public EffectControl effectControl;
    public GameContext gameContext;

    void Start()
    {
        if (gameContext == null)
        {
            gameContext = FindObjectOfType<GameContext>();
        }
        if (effectControl == null)
        {
            effectControl = FindObjectOfType<EffectControl>();
        }
    }

    public void useCard(List<Card> cards)
    {
        List<string> suits = new List<string>();
        int totalatt = 0;
        foreach(var card in cards)
        {
            totalatt += card.att;
            suits.Add(card.suit);
        }
        foreach(var suit in suits)
        {
            if (suit.Equals("♠"))
            {
                effectControl.Defense(totalatt);
            }
            else if(suit.Equals("♥"))
            {
                effectControl.RecoverCard(totalatt);
            }
            else if(suit.Equals("♦"))
            {
                effectControl.DrawCards(totalatt);
            }
            else if(suit.Equals("♣"))
            {
                effectControl.DoubleDamage(totalatt);
            }
        }

        gameContext.CurrentBoss.currenthealth = gameContext.CurrentBoss.currenthealth - totalatt;

    }

    public void FoldCard(List<Card> card)
    {

    }

}
