using System;
using UnityEngine;

public class MovableStick : CommonStick
{
    [SerializeField]
    Transform _movableRoot;
    [SerializeField]
    Transform _leftPoint;
    [SerializeField]
    Transform _rightPoint;
    [SerializeField]
    float _timer;
    [SerializeField]
    float _speed;

    CatController _catController;
    Guid? _id;
    Transform _closestPoint;

    bool _toTheRight;
    float _currentTimer;

    void Start()
    {
        _catController = FindObjectOfType<CatController>();
        _currentTimer = _timer;
    }

    void Update()
    {
        if (_currentTimer > 0)
            _currentTimer -= Time.unscaledDeltaTime;

        _movableRoot.position = Vector2.MoveTowards(_movableRoot.position,
            _toTheRight ? _rightPoint.position : _leftPoint.position,
            _speed * Time.unscaledDeltaTime);

        if (_currentTimer <= 0)
        {
            _currentTimer = _timer;
            _toTheRight = !_toTheRight;
        }

        if (_id != null)
        {
            if (_catController.id == _id)
            {
                _catController.SetPosition(_closestPoint);
            }
            else
            {
                _id = null;
                _closestPoint = null;
            }
        }
    }

    public Guid SetHanging()
    {
        var id = Guid.NewGuid();
        _id = id;
        return id;
    }

    public override Transform GetClosestPoint(Vector2 position)
    {
        _closestPoint = base.GetClosestPoint(position);
        return _closestPoint;
    }
}
