using UnityEngine;

public class CommonStick : MonoBehaviour
{
    [SerializeField]
    Transform[] _points;

    public virtual Transform GetClosestPoint(Vector2 position)
    {
        if (_points.Length == 0)
            return transform;

        float distance = float.MaxValue;
        Transform closestPoint = _points[0];
        foreach (Transform point in _points)
        {
            var newDistance = Vector2.Distance(point.position, position);
            if (newDistance <= distance)
            {
                distance = newDistance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
}
