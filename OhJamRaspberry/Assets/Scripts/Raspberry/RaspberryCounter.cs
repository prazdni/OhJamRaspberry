using TMPro;
using UnityEngine;

public class RaspberryCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _raspberryCount;

    RaspberryManager _raspberryManager;

    void Start()
    {
        _raspberryManager = FindObjectOfType<RaspberryManager>();
        SetRaspberry();

        if (_raspberryManager)
            _raspberryManager.onRaspberryAdded += SetRaspberry;
    }

    void OnDestroy()
    {
        if (_raspberryManager)
            _raspberryManager.onRaspberryAdded -= SetRaspberry;
    }

    void SetRaspberry()
    {
        if (_raspberryManager)
            _raspberryCount.text = _raspberryManager.raspberriesCount.ToString();
    }
}
