using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float _offsetY;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _pointingDelay;
    [SerializeField]
    float _flyingDelay;

    CatController _catController;
    float _currentDelay;

    Vector3 _velocity;

    void Start()
    {
        _currentDelay = _pointingDelay;

        _catController = FindObjectOfType<CatController>();
        transform.position = new Vector3(0, _catController.transform.position.y, -10);
        _catController.onStateChanged += SetDelay;
    }

    void OnDestroy()
    {
        _catController.onStateChanged -= SetDelay;
    }

    void Update()
    {
        if (_currentDelay >= 0)
        {
            _currentDelay -= Time.unscaledDeltaTime;
            return;
        }

        var target = new Vector3(0, _catController.transform.position.y +_offsetY, -10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, _speed);
    }

    void SetDelay(CatMovementController.CatState catState)
    {
        switch (catState)
        {
            case CatMovementController.CatState.Hanging:
                _currentDelay = _pointingDelay;
                break;
            case CatMovementController.CatState.Flying:
                _currentDelay = _flyingDelay;
                break;
        }
    }
}
