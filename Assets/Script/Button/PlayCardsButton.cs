using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardsButton : MonoBehaviour
{
    public CardManager cardManager;

    //按钮点击事件
    public void OnPlayButtonClicked()
    {
        if (cardManager != null)
        {
            cardManager.PlaySelectedCards();
        }
        else
        {
            Debug.LogError("CardManager 未设置！");
        }
    }
}
