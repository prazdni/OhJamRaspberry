using UnityEngine;

public class GameFinished : MonoBehaviour
{
    [SerializeField]
    Collider2D _collider2D;

    void OnTriggerExit(Collider other)
    {
        var catMovementController = other.GetComponent<CatMovementController>();
        if (catMovementController)
        {
            catMovementController.FinishGame();
            _collider2D.isTrigger = false;
        }
    }
}
