using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;

public class CardManager : MonoBehaviour
{

    public GameObject cardPrefab; //卡牌预制件
    public RectTransform handArea; //手牌区
    public RectTransform playArea; //用牌区
    public GameContext gameContext; //游戏上下文
    public TextMeshProUGUI CardDeckCount; //抽牌堆剩余牌数
    public TextMeshProUGUI FoldDeckCount; //弃牌数

    private List<CardDisplay> handUI = new List<CardDisplay>(); //手牌UI
    private List<CardDisplay> selectedCards = new List<CardDisplay>(); //被选中的卡牌
    private List<Card> selectedCard = new List<Card>(); //被选中的卡牌

    public UseCard useCard;

    private static float moveDis = 50f; //被点击后移动的距离
    public TextMeshProUGUI warningText; // 错误提示文本组件
    public GameObject warningTextParent; //错误文本的父组件
    public float warningTimer = 0f; // 提示显示时长（秒）

    public GameObject cardbutton; //出牌按钮
    public GameObject foldbutton; //弃牌按钮
    public bool confirmDiscardButtonClicked = false;
    public enum PlayerActionPhase
    {
        PlayCard,   // 出牌阶段
        Discard     // 弃牌阶段
    }
    public PlayerActionPhase currentPhase = PlayerActionPhase.PlayCard;

    // Start is called before the first frame update
    void Start()
    {
        if (useCard == null)
        {
            useCard = FindObjectOfType<UseCard>();
        }
        warningTextParent.SetActive(false);
        foldbutton.SetActive(false);
        cardbutton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (warningTimer > 0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                warningTextParent.SetActive(false);
            }
        }
    }

    // 显示手牌
    public void RefreshHandUI()
    {
        foreach (var display in handUI)
        {
            if (display != null)
                DestroyImmediate(display.gameObject);
        }

        handUI.Clear();
        selectedCards.Clear();

        gameContext.CurrentPlayer.SortBySuit();
        Player player = gameContext.CurrentPlayer;
        foreach (var card in player.hand)
        {
            GameObject obj = Instantiate(cardPrefab, handArea);
            CardDisplay display = obj.GetComponent<CardDisplay>();
            display.Init(card, this);
            handUI.Add(display);

            //绑定点击事件
            var button = obj.GetComponentInChildren<UnityEngine.UI.Button>();
            if (button != null)
            {
                //button.onClick.AddListener(() => OnClickCard(display));
                CardDisplay current = display; // 避免闭包捕获错误
                button.onClick.AddListener(() => OnClickCard(current));
            }
        }

        //调整显示
        HandManager handManager = handArea.GetComponent<HandManager>();
        if (handManager != null)
        {
            handManager.SortYourHand();
        }

        //文字显示
        if (CardDeckCount != null)
            CardDeckCount.text = $"剩余牌数:{gameContext.PlayerDeck.Count}";

        if (FoldDeckCount != null)
            FoldDeckCount.text = $"弃牌数:{gameContext.FolderDeck.Count}";


    }

    // 点击卡牌
    public void OnClickCard(CardDisplay display)
    {
        if (selectedCards.Contains(display))
        {
            // 已选 → 取消选中，下降
            selectedCards.Remove(display);
            selectedCard.Remove(display.card);
            MoveCard(display, false);
        }
        else
        {
            // 未选 → 选中，上升

            //条件判定
            if (currentPhase != PlayerActionPhase.Discard)  //非弃牌时进行
            {
                if (selectedCard.Count > 0)
                {
                    int totalvalue = 0;
                    bool hasA = false;
                    string firstNonAPoint = null;
                    string newCardPoint = display.card.point;

                    foreach (var card in selectedCard)
                    {
                        totalvalue += card.att;
                        if (card.point.Equals("A"))
                            hasA = true;
                        else if (firstNonAPoint == null)
                            firstNonAPoint = card.point;
                    }

                    if (firstNonAPoint != null && (newCardPoint != firstNonAPoint && !(newCardPoint.Equals("A"))))
                    {
                        //如果第一张不是A，且之后选中不相同
                        warningText.text = "连招只能选择A或相同点数的牌";
                        warningTextParent.SetActive(true);
                        warningTimer = 2f; // 显示2秒
                        return;
                    }
                    if (firstNonAPoint == null && selectedCard.Count >= 2 && !(newCardPoint.Equals("A")))
                    {
                        warningText.text = "连招只能选择A或相同点数的牌";
                        warningTextParent.SetActive(true);
                        warningTimer = 2f;
                        return;
                    }
                    if ((totalvalue + display.card.att) > 10)
                    {
                        warningText.text = "相同点数的牌总和不能大于10";
                        warningTextParent.SetActive(true);
                        warningTimer = 2f; // 显示2秒
                        return;
                    }
                }
            }

            selectedCards.Add(display);
            selectedCard.Add(display.card);
            MoveCard(display, true);
        }
    }

    //控制文本显示
    IEnumerator HideWarningAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        warningText.gameObject.SetActive(false);
    }

    // 上升/下降动画（瞬移）
    private void MoveCard(CardDisplay display, bool isUp)
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        if (rt == null) return;

        Vector2 pos = rt.anchoredPosition;
        pos.y += isUp ? moveDis : -moveDis;
        rt.anchoredPosition = pos;
    }

    // 玩家点击“出牌”按钮
    public void PlaySelectedCards()
    {
        if (selectedCards.Count == 0) return;

        Player player = gameContext.CurrentPlayer;

        // 将选中的卡牌从手牌移动到弃牌堆
        foreach (var display in selectedCards)
        {
            // 从玩家手牌中移除
            player.hand.Remove(display.card);

            // 加入弃牌堆
            gameContext.FolderDeck.Add(display.card);

            // 卡牌UI移入弃牌区
            display.transform.SetParent(playArea, false);
            handUI.Remove(display);
        }


        // 清空选中列表
        selectedCards.Clear();

        // 刷新手牌UI
        RefreshHandUI();

        //行动逻辑
        Debug.Log(selectedCard.Count);
        useCard.useCard(selectedCard);
        gameContext.state.doAction(gameContext);
        selectedCard.Clear();
    }

    public void confirmclick()
    {
        confirmDiscardButtonClicked = true;
        foldbutton.SetActive(false);
        cardbutton.SetActive(true);

    }

    public IEnumerator PlayerDiscardForBoss(int requiredValue)
    {
        foldbutton.SetActive(true);
        Debug.Log($"请选中牌丢弃，总点数 ≥ {requiredValue}");

        bool completed = false;

        // 显示UI提示
        warningText.text = $"请选择牌丢弃,总点数>={requiredValue}";
        warningTextParent.SetActive(true);
        warningTimer = 3f;

        currentPhase = PlayerActionPhase.Discard;

        while (!completed)
        {
            yield return null; // 等待每一帧

            // 计算选中牌总点数
            int totalValue = 0;
            foreach (var card in selectedCards)
                totalValue += card.card.att;

            // 如果玩家点击确认按钮
            if (confirmDiscardButtonClicked)
            {
                if (totalValue >= requiredValue)
                {
                    // 丢弃选中牌
                    foreach (var display in selectedCards)
                    {
                        gameContext.CurrentPlayer.hand.Remove(display.card);
                        gameContext.FolderDeck.Add(display.card);
                        display.transform.SetParent(playArea, false); // 或者专门的弃牌区
                        handUI.Remove(display);
                    }

                    selectedCards.Clear();
                    selectedCard.Clear();
                    RefreshHandUI();

                    completed = true; // 结束协程
                }
                else
                {
                    warningText.text = $"选中牌总点数不足{requiredValue}";
                    warningTextParent.SetActive(true);
                    warningTimer = 3f; // 显示错误提示
                }

                foldbutton.SetActive(true);
                cardbutton.SetActive(false);
                confirmDiscardButtonClicked = false; // 重置
            }
        }

        warningTextParent.SetActive(false);
        Debug.Log("玩家完成Boss丢牌");
        foldbutton.SetActive(false);
        cardbutton.SetActive(true);
        confirmDiscardButtonClicked = true;
        currentPhase = PlayerActionPhase.PlayCard;
        selectedCards.Clear();
        selectedCard.Clear();
        RefreshHandUI();
    }


}
