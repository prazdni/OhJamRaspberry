using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSettings", menuName = "SkinSettings", order = 1)]
public class SkinSettings : ScriptableObject
{
    [Serializable]
    public class Skin
    {
        public string id;
        public Sprite sample;
        public Sprite body;
        public Sprite leftPaw;
        public Sprite rightPaw;
    }

    [Serializable]
    public class BuySkin : Skin
    {
        public bool isBought;
        public int raspberriesCount;
    }

    public Skin defaultSkin;
    public BuySkin[] buySkins;

    public BuySkin GetBuySkin(string id)
    {
        return Array.Find(buySkins, skin => skin.id == id);
    }
}
