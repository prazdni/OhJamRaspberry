using UnityEngine;

public class LevelController : MonoBehaviour
{
    CatController _catController;

    void Start()
    {
        _catController = FindObjectOfType<CatController>();
    }

    void OnMouseDown()
    {
        _catController.SetHanging();
    }
}
