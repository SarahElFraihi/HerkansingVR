using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2f;
    public float maxRotationAngle = 90f; // ±90° from original forward

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 targetPosition = player.position + Vector3.up * 1.6f; // 1.6 meters = average head height
        Vector3 direction = targetPosition - transform.position;
        Vector3 directionToPlayer = direction.normalized;

        if (directionToPlayer == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        Quaternion limitedRotation = ClampRotation(originalRotation, targetRotation, maxRotationAngle);

        transform.rotation = Quaternion.Slerp(transform.rotation, limitedRotation, Time.deltaTime * rotationSpeed);
    }

    Quaternion ClampRotation(Quaternion original, Quaternion target, float maxAngle)
    {
        float angle = Quaternion.Angle(original, target);
        if (angle > maxAngle)
        {
            target = Quaternion.RotateTowards(original, target, maxAngle);
        }
        return target;
    }
}

