using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _following;
    
    [SerializeField] private float _rotationAngelX;
    [SerializeField] private float _distanceFromTarget;
    [SerializeField] private float _yOffset;

    private void LateUpdate()
    {
        if (_following == null)
        {
            return;
        }

        var targetRotation = Quaternion.Euler(_rotationAngelX, 0, 0);

        var targetPosition = targetRotation * new Vector3(0, 0, -_distanceFromTarget) + GetFollowingPosition();

        transform.rotation = targetRotation;
        transform.position = targetPosition;
    }

    public void SetFollowingTarget(GameObject targetGameObject) => _following = targetGameObject.transform;

    private Vector3 GetFollowingPosition()
    {
        var followingPosition = _following.position;
        followingPosition.y += _yOffset;
        return followingPosition;
    }
}
