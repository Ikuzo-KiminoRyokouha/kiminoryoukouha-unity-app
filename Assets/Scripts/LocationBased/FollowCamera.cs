using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _distance = 1f;

    private void Update()
    {
        Vector3 direction = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 targetPosition = _cameraTransform.position + (direction * _distance);
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        float t = Time.deltaTime * _speed;
        Vector3 position = Vector3.Lerp(transform.position, targetPosition, t);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
        transform.SetPositionAndRotation(position, rotation);
    }
}
