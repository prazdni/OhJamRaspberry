using UnityEngine;

public class GroundController : MonoBehaviour
{
    CatController _catController;

    void Start()
    {
        _catController = FindObjectOfType<CatController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CatMovementController>())
            _catController.Restart();
    }
}
