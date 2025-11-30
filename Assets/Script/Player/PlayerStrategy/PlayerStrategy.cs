using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerStrategy
{
    void doaction(GameContext gameContext);
}

public class TestAction : PlayerStrategy
{
    public void doaction(GameContext gameContext)
    {
        gameContext.CurrentBoss.currenthealth = 0;
    }
}

