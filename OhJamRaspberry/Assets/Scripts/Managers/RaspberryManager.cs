using System;
using System.Collections.Generic;
using UnityEngine;

public class RaspberryManager : MonoBehaviour
{
    public event Action onRaspberriesChanged;

    public int raspberriesCount => _raspberries.Count - _consumedRaspberriesCount;

    HashSet<string> _raspberries;
    int _consumedRaspberriesCount;

    void Awake()
    {
        RaspberryManager[] objectsOfType = FindObjectsOfType<RaspberryManager>();
        foreach (RaspberryManager objectOfType in objectsOfType)
        {
            if (objectOfType != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        _raspberries = new HashSet<string>();
    }

    public bool IsCollected(string id)
    {
        return _raspberries.Contains(id);
    }

    public void AddRaspberry(string id)
    {
        _raspberries.Add(id);
        onRaspberriesChanged?.Invoke();
    }

    public void ConsumeRaspberries(int amount)
    {
        _consumedRaspberriesCount += amount;
        onRaspberriesChanged?.Invoke();
    }
}
