using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 _initialPosition;
    [SerializeField]
    float _offsetY;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _pointingDelay;
    [SerializeField]
    float _flyingDelay;
    [SerializeField]
    Transform _topPoint;
    [SerializeField]
    Transform _bottomPoint;

    CatController _catController;
    float _currentDelay;

    Vector3 _velocity;

    void Start()
    {
        _currentDelay = _pointingDelay;

        _catController = FindObjectOfType<CatController>();
        transform.position = _initialPosition;
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


        //float topDistance = Vector2.Distance(transform.position, _topPoint.position);
        //float bottomDistance = Vector2.Distance(transform.position, _bottomPoint.position);
        //if (topDistance < _thresholdY || bottomDistance < _thresholdY)
        //    return;

        float targetY = Mathf.Clamp(_catController.transform.position.y + _offsetY,
            _bottomPoint.position.y,
            _topPoint.position.y);

        var target = new Vector3(0, targetY, -10);
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
