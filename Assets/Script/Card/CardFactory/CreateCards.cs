using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCards : MonoBehaviour
{

    public TextAsset cardData;
    public List<Card> bosscardList = new List<Card>();
    public List<Card> normalcardList = new List<Card>();
    public List<Card> jokercardList = new List<Card>();


    // Start is called before the first frame update
    void Start()
    {
        LoadCards();
        //TestCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCards()
    {
        string[] dataRow = cardData.text.Split('\n');  //读取csv每行
        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(','); //分隔每行的数据
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "Boss")
            {
                //新建Boss卡
                string point = rowArray[1];
                string suit = rowArray[2];
                int att = int.Parse(rowArray[3]);
                BossCard bosscard = new BossCard(suit,point,att);
                bosscardList.Add(bosscard);
            }
            else if (rowArray[0] == "Normal")
            {
                //新建正常卡
                string point = rowArray[1];
                string suit = rowArray[2];
                int att = int.Parse(rowArray[3]);
                NormalCard normalcard = new NormalCard(suit, point, att);
                normalcardList.Add(normalcard);
            }
            else if (rowArray[0] == "Joker")
            {
                //新建小丑牌
                string point = rowArray[1];
                string suit = rowArray[2];
                int att = int.Parse(rowArray[3]);
                JokerCard jokercard = new JokerCard(suit, point, att);
                jokercardList.Add(jokercard);
            }
        }
    }

    //测试用
    /*public void TestCard()
    {
        foreach(var item in bosscardList)
        {
            Debug.Log("Boss卡:" + item.point.ToString() + item.suit.ToString());
        }
        foreach (var item in normalcardList)
        {
            Debug.Log("Normal卡:" + item.point.ToString() + item.suit.ToString());
        }
    }*/


}
