using UnityEngine;

public class CatMovementController : MonoBehaviour
{
    public CatState catState => _catState;

    public class Info
    {
        public Vector2 force;
    }

    public enum CatState
    {
        Hanging,
        Flying
    }

    [SerializeField]
    Rigidbody2D _rigidbody2D;

    CatState _catState;

    public void SetState(CatState state, Info info)
    {
        _catState = state;

        switch (_catState)
        {
            case CatState.Hanging:
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                break;

            case CatState.Flying:
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody2D.AddForce(info.force, ForceMode2D.Impulse);
                break;
        }
    }
}
