using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public GameObject Boss; //当前Boss
    public RectTransform BossArea;
    public TextMeshProUGUI BossDeck; //剩余Boss数
    public TextMeshProUGUI bossHp; //boss血量
    public TextMeshProUGUI bossAtt; //boss攻击

    public GameContext gameContext;
    public BossCard currentboss;

    private GameObject currentBossObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(GameContext context)
    {
        if (gameContext == null)
        {
            gameContext = FindObjectOfType<GameContext>();
        }

        if (gameContext != null)
        {
            currentboss = gameContext.CurrentBoss;
        }



        RefreshBossUI();
    }

    //更新Boss信息
    public void RefreshBossUI()
    {
        if (gameContext == null || gameContext.CurrentBoss == null)
        {
            Debug.LogWarning("BossManager: CurrentBoss 未设置！");
            return;
        }

        // 清理旧的Boss显示
        if (currentBossObj != null)
            Destroy(currentBossObj);

        // 实例化Boss卡牌
        currentboss = gameContext.CurrentBoss;
        currentBossObj = Instantiate(Boss, BossArea);
        CardDisplay display = currentBossObj.GetComponent<CardDisplay>();
        if (display != null)
            display.Init(currentboss, this);

        // 更新UI显示
        if (bossHp != null)
            bossHp.text = $"生命:{currentboss.currenthealth}/{currentboss.health}";

        if (bossAtt != null)
            bossAtt.text = $"攻击:{currentboss.att}";

        if(BossDeck != null)
            BossDeck.text = $"剩余Boss数:{gameContext.TotalBossDeck.Count}";


    }

    public void UpdateBossHp()
    {
        if(bossHp != null)
           bossHp.text = $"生命:{currentboss.currenthealth}/{currentboss.health}";
    }

}
