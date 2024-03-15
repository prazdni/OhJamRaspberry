using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool hasLastLevel => _lastLevel.HasValue;

    int? _lastLevel;

    void Awake()
    {
        GameManager[] objectsOfType = FindObjectsOfType<GameManager>();
        foreach (GameManager objectOfType in objectsOfType)
        {
            if (objectOfType != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void StartFromLastLevel()
    {
        if (!_lastLevel.HasValue)
            _lastLevel = 1;

        SceneManager.LoadScene(_lastLevel.Value);
    }

    public void GoNext()
    {
        _lastLevel++;
        StartFromLastLevel();
    }
}
