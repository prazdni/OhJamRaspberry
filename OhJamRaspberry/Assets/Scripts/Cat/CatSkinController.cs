using UnityEngine;

public class CatSkinController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _body;
    [SerializeField]
    SpriteRenderer _leftPaw;
    [SerializeField]
    SpriteRenderer _rightPaw;

    SkinManager _skinManager;

    public void Start()
    {
        _skinManager = FindObjectOfType<SkinManager>();
        if (_skinManager)
        {
            SkinSettings.Skin chosenSkin = _skinManager.chosenSkin;
            _body.sprite = chosenSkin.body;
            _leftPaw.sprite = chosenSkin.leftPaw;
            _rightPaw.sprite = chosenSkin.rightPaw;
        }
    }
}
