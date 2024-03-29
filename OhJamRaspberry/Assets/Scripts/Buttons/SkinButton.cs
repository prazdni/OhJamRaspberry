using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField]
    Button _button;
    [SerializeField]
    GameObject _chosen;
    [SerializeField]
    GameObject _available;
    [SerializeField]
    TextMeshProUGUI _raspberryCount;
    [SerializeField]
    Image _spriteRenderer;
    [SerializeField]
    GameObject _raspberry;

    SkinSettings.Skin _skin;
    SkinManager _skinManager;

    public void OnDestroy()
    {
        _button.onClick.RemoveListener(ChooseSkin);
        _skinManager.onChanged -= SetSkin;
    }

    public void SetSkin(SkinManager skinManager, SkinSettings.Skin skin)
    {
        _skinManager = skinManager;
        _skin = skin;
        _spriteRenderer.sprite = _skin.sample;

        SetSkin();

        _button.onClick.AddListener(ChooseSkin);
        _skinManager.onChanged += SetSkin;
    }

    void ChooseSkin()
    {
        _skinManager.ChooseSkin(_skin.id);
    }

    void SetSkin()
    {
        _chosen.SetActive(_skinManager.chosenSkin.id == _skin.id);
        SetAvailable();
    }

    void SetAvailable()
    {
        _raspberry.SetActive(false);
        _available.SetActive(true);
        _raspberryCount.text = "";
        if (_skin is SkinSettings.BuySkin buySkin)
        {
            _raspberryCount.text = buySkin.isBought ? "" : buySkin.raspberriesCount.ToString();
            _available.SetActive(buySkin.isBought);
            _raspberry.SetActive(!buySkin.isBought);
        }
    }
}
