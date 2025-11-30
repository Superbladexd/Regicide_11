using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    /*public TextMeshProUGUI point; //点数
    public TextMeshProUGUI suit; //花色
    public TextMeshProUGUI att; //攻击力
    public TextMeshProUGUI health; //血量*/
    public Image BGimage; //卡图
    public Card card;

    public Button clickblock;
    public CardManager cardManager;
    public BossManager bossManager;
    private bool isSelected = false;


    //初始化卡牌数据
    public void Init(Card data,CardManager cardmanager)
    {
        cardManager = cardmanager;
        card = data;
        ShowCard();

        /*if (clickblock != null)
            clickblock.onClick.AddListener(OnClickCard);*/

    }

    public void Init(Card data, BossManager bossmanager)
    {
        bossManager = bossmanager;
        card = data;
        ShowCard();


    }

    // Start is called before the first frame update
    void Start()
    {

        if (card != null)
            ShowCard();

        /*if (clickblock != null)
            clickblock.onClick.AddListener(OnClickCard);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCard()
    {
        if (card == null) return;

        /*point.text = card.point;
        suit.text = card.suit;
        att.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
        if (card is BossCard)
        {
            var boss = card as BossCard;
            att.text = boss.currentvalue.ToString();
            health.text = boss.currenthealth.ToString();
            att.gameObject.SetActive(true);
            health.gameObject.SetActive(true);
        }*/

        string cardName;

        if (card.suit.Equals("♠"))
        {
            cardName = "HT_" + card.point;
        }
        else if (card.suit.Equals("♣"))
        {
            cardName = "MH_" + card.point;
        }
        else if (card.suit.Equals("♥"))
        {
            cardName = "HX_" + card.point;
        }
        else if (card.suit.Equals("♦"))
        {
            cardName = "FP_" + card.point;
        }
        else
        {
            cardName = "JOKER";
        }

        Sprite s = CardSpriteManager.Instance.GetCardSprite(cardName);

        if (s != null)
        {
            BGimage.sprite = s;
        }

    }

    /*public void SetPlayable(bool canPlay)
    {
        // 不可出时 → 背景变灰 + 按钮禁用
        BGimage.color = canPlay ? Color.white : Color.gray;
        clickblock.interactable = canPlay;
    }

    // 玩家点击卡牌选择
    public void OnClickCard()
    {
        isSelected = !isSelected;
        BGimage.color = isSelected ? Color.yellow : Color.white;

        if (isSelected)
            cardManager.SelectCard(this);
        else
            cardManager.DeselectCard(this);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    // 回到手牌
    public void ReturnToHand(Transform handArea)
    {
        transform.SetParent(handArea, false);
        SetPlayable(true);
    }*/

}
