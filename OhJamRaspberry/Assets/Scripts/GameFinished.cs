using UnityEngine;

public class GameFinished : MonoBehaviour
{
    [SerializeField]
    Collider2D _collider2D;
    [SerializeField]
    GameObject _theEnd;

    void Start()
    {
        _theEnd.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var catMovementController = other.GetComponent<CatMovementController>();
        if (catMovementController)
        {
            catMovementController.FinishGame();
            _theEnd.SetActive(true);
            _collider2D.isTrigger = false;
        }
    }
}
