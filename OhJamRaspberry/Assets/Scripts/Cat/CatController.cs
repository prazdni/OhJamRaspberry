using System;
using Sticks;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public event Action<CatMovementController.CatState> onStateChanged;

    [SerializeField]
    Transform _bodyTransform;
    [SerializeField]
    Transform _pawsTransform;
    [SerializeField]
    Transform _pawsBox;
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    CatMovementController _catMovementController;
    [SerializeField]
    float _forceApply;
    [SerializeField]
    float _threshold;
    [SerializeField]
    float _maxMagnitude;
    [SerializeField]
    Vector2 _initialPosition;
    [SerializeField]
    float _maxAngle;
    [SerializeField]
    Vector2 _overlapBox;

    Vector2? _force;
    Guid? _guid;

    bool _isDown;

    void Start()
    {
        _force = null;
        transform.position = _initialPosition;
        ResetTransforms();

        _catMovementController.SetState(CatMovementController.CatState.Hanging, null);
    }

    void OnMouseDrag()
    {
        if (_catMovementController.catState == CatMovementController.CatState.Hanging)
        {
            if (_isDown)
            {
                _isDown = false;
                return;
            }

            var mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 catPosition = transform.position;
            Vector2 dir = (catPosition - worldPoint);

            Vector3 direction = (catPosition - worldPoint).normalized;
            float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
            angle -= 90;

            if (Mathf.Abs(angle) <= _maxAngle)
            {
                var rotation = new Quaternion { eulerAngles = new Vector3(0,0,angle) };
                _bodyTransform.rotation = rotation;

                if (dir.magnitude < _threshold)
                {
                    _force = null;
                    _spriteRenderer.size = new Vector2(1, 1);
                    return;
                }

                if (dir.magnitude <= _maxMagnitude)
                {
                    _force = dir * _forceApply;
                    _spriteRenderer.size = new Vector2(_spriteRenderer.size.x, dir.magnitude + 1);
                }
            }
        }
    }

    void OnMouseDown()
    {
        switch (_catMovementController.catState)
        {
            case CatMovementController.CatState.Flying:
                SetHanging();
                break;
        }
    }

    void OnMouseUp()
    {
        switch (_catMovementController.catState)
        {
            case CatMovementController.CatState.Hanging:
                if (_isDown)
                {
                    _isDown = false;
                    return;
                }

                SendFly();
                break;
        }
    }

    public void SetHanging()
    {
        if (_catMovementController.catState == CatMovementController.CatState.Flying)
        {
            Collider2D[] contacts = Physics2D.OverlapBoxAll(_pawsBox.position,
                _overlapBox,
                _pawsTransform.rotation.eulerAngles.z);

            foreach (Collider2D contact in contacts)
            {
                var commonStick = contact.transform.GetComponent<CommonStick>();
                if (commonStick != null)
                {
                    if (commonStick is BreakableStick breakableStick)
                    {
                        if (breakableStick.canBeHanging)
                        {
                            _guid = breakableStick.SetHanging();
                            Hang(breakableStick);
                        }
                    }
                    else
                    {
                        Hang(commonStick);
                    }

                    break;
                }
            }
        }
    }

    public void Restart()
    {
        transform.position = _initialPosition;
        ResetTransforms();

        _catMovementController.SetState(state: CatMovementController.CatState.Hanging, info: null);
    }

    public void RemoveFromStick(Guid guid)
    {
        if (_guid == guid)
        {
            _force = Vector2.zero;
            SendFly();
        }
    }

    void SendFly()
    {
        _spriteRenderer.size = new Vector2(1, 1);

        if (_force != null)
        {
            _spriteRenderer.color = Color.white;
            _catMovementController.SetState(CatMovementController.CatState.Flying, new CatMovementController.Info
            {
                force = _force.Value
            });
            _pawsTransform.rotation = _bodyTransform.rotation;

            _force = null;
            _guid = null;

            onStateChanged?.Invoke(CatMovementController.CatState.Flying);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
    }

    void Hang(CommonStick commonStick)
    {
        _isDown = true;

        transform.position = commonStick.GetClosestPoint(_pawsBox.position) + Vector2.down * 0.75f;
        ResetTransforms();

        _catMovementController.SetState(CatMovementController.CatState.Hanging, null);
        onStateChanged?.Invoke(CatMovementController.CatState.Hanging);
    }

    void ResetTransforms()
    {
        transform.localRotation = Quaternion.identity;
        _bodyTransform.localRotation = Quaternion.identity;
        _pawsTransform.localRotation = Quaternion.identity;
    }
}
