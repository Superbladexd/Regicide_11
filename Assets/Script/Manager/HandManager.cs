using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private float W;        // 容器宽度
    private float D = 100f; // 卡牌之间的初始间隔（像素单位）
    private float d;        // 卡牌之间的实际间隔
    private int n;          // 卡牌数量

    void Start()
    {
        // 获取父容器的宽度
        W = GetComponent<RectTransform>().rect.width;
        d = D;
    }

    // 排列手牌
    public void SortYourHand()
    {

        if (transform.childCount == 0)
            return;

        n = transform.childCount;
        d = D;

        // 自动缩小间距
        if ((n - 1) * d > W * 0.9f)
        {
            d = (W * 0.9f) / (n - 1);
        }

        // 起始位置以容器中心为基准
        float startX =  -0.5f * d * (n-1); 


        for (int i = 0; i < n; i++)
        {
            RectTransform cardRect = transform.GetChild(i).GetComponent<RectTransform>();
            if (cardRect != null)
            {
                cardRect.anchoredPosition = new Vector2(startX + i * d, 0);
                cardRect.SetSiblingIndex(i);
            }
        }
    }
}
