using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public event Action onChanged;

    const string path = "Settings/SkinSettings";

    public SkinSettings.Skin chosenSkin => _chosenSkin;

    SkinSettings _skinSettings;
    RaspberryManager _raspberryManager;
    SkinSettings.Skin _chosenSkin;

    void Awake()
    {
        SkinManager[] objectsOfType = FindObjectsOfType<SkinManager>();
        foreach (SkinManager objectOfType in objectsOfType)
        {
            if (objectOfType != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        _skinSettings = Resources.Load<SkinSettings>(path);
        _chosenSkin = _skinSettings.defaultSkin;
    }

    void Start()
    {
        _raspberryManager = FindObjectOfType<RaspberryManager>();
    }

    public SkinSettings.Skin[] GetAllSkins()
    {
        var skins = new List<SkinSettings.Skin> { _skinSettings.defaultSkin };
        skins.AddRange(_skinSettings.buySkins);
        return skins.ToArray();
    }

    public void ChooseSkin(string id)
    {
        if (_skinSettings.defaultSkin.id == id)
        {
            _chosenSkin = _skinSettings.defaultSkin;
            onChanged?.Invoke();
            return;
        }

        SkinSettings.BuySkin buySkin = _skinSettings.GetBuySkin(id);
        if (buySkin.isBought)
        {
            _chosenSkin = buySkin;
            onChanged?.Invoke();
            return;
        }

        if (buySkin.raspberriesCount <= _raspberryManager.raspberriesCount)
        {
            _raspberryManager.ConsumeRaspberries(buySkin.raspberriesCount);
            buySkin.isBought = true;
            _chosenSkin = buySkin;
            onChanged?.Invoke();
        }
    }
}
