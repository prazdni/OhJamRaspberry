using UnityEngine;

public class InputController : MonoBehaviour
{
    CatController _catController;

    void Start()
    {
        _catController = FindObjectOfType<CatController>();
    }

    void Update()
    {
        if (_catController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _catController.MouseDown();
            }
            else if (Input.GetMouseButton(0))
            {
                _catController.MouseDrag();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _catController.MouseUp();
            }
        }
    }
}
