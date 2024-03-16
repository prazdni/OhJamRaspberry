using UnityEngine;

public class FinishLevelController : MonoBehaviour
{
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_gameManager)
            _gameManager.GoNext();
    }
}
