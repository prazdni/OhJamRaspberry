using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    Button _button;

    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _button.onClick.AddListener(Quit);
    }

    void Quit()
    {
        if (_gameManager)
            _gameManager.Quit();
        else
            SceneManager.LoadScene(0);
    }
}
