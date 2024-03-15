using System;
using System.Collections.Generic;
using UnityEngine;

public class RaspberryManager : MonoBehaviour
{
    public event Action onRaspberryAdded;

    public int raspberriesCount => _raspberries.Count;

    HashSet<string> _raspberries;

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
        onRaspberryAdded?.Invoke();
    }
}
