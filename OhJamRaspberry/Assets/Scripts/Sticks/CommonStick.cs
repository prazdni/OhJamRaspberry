using UnityEngine;

public class CommonStick : MonoBehaviour
{
    [SerializeField]
    Transform[] _points;

    public Vector2 GetClosestPoint(Vector2 position)
    {
        if (_points.Length == 0)
            return transform.position;

        float distance = float.MaxValue;
        Vector2 closestPoint = _points[0].position;
        foreach (Transform point in _points)
        {
            var newDistance = Vector2.Distance(point.position, position);
            if (newDistance <= distance)
            {
                distance = newDistance;
                closestPoint = point.position;
            }
        }

        return closestPoint;
    }
}
