using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteManager : MonoBehaviour
{
    public static CardSpriteManager Instance;

    private Dictionary<string, Sprite> spriteDict;

    private void Awake()
    {
        Instance = this;
        spriteDict = new Dictionary<string, Sprite>();

        // º”‘ÿÀ˘”–ø®≈∆ Sprite
        Sprite[] allSprites = Resources.LoadAll<Sprite>("Cards");
        foreach (var s in allSprites)
        {
            spriteDict[s.name] = s;
        }
    }

    public Sprite GetCardSprite(string spriteName)
    {
        if (spriteDict.ContainsKey(spriteName))
            return spriteDict[spriteName];
        return null;
    }
}
