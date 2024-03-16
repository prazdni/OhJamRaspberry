using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField]
    Transform _root;
    [SerializeField]
    SkinButton _skinButton;

    SkinManager _skinManager;

    public void Start()
    {
        _skinManager = FindObjectOfType<SkinManager>();
        if (_skinManager)
        {
            SkinSettings.Skin[] skins = _skinManager.GetAllSkins();
            foreach (SkinSettings.Skin skin in skins)
            {
                SkinButton skinButton = Instantiate(_skinButton, _root);
                skinButton.SetSkin(_skinManager, skin);
            }
        }
    }
}
