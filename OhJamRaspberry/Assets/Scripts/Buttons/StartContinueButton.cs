using UnityEngine;
using UnityEngine.UI;

public class StartContinueButton : MonoBehaviour
{
    [SerializeField]
    Button _button;
    [SerializeField]
    GameObject _startText;
    [SerializeField]
    GameObject _continueText;

    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _startText.SetActive(!_gameManager.hasLastLevel);
        _continueText.SetActive(_gameManager.hasLastLevel);
        _button.onClick.AddListener(_gameManager.StartFromLastLevel);
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveListener(_gameManager.StartFromLastLevel);
    }
}
