using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform leverHandle;
    [SerializeField] private Vector3 moveDirection = Vector3.down;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float leverAngleThreshold = -45f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = transform.position;
    }

    void Update()
    {
        float leverAngle = Vector3.Angle(leverHandle.up, transform.up);

        if (leverAngle >= leverAngleThreshold)
        {
            targetPosition = originalPosition + moveDirection.normalized * moveDistance;
        }
        else
        {
            targetPosition = originalPosition;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
