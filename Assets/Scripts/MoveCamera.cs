using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform leverHandle;
    [SerializeField] private Vector3 moveDirection = Vector3.down;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float leverAngleThreshold = 45f;
    [SerializeField] private float leverStartAngleThreshold = 15f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float positionThreshold = 0.01f;
    [SerializeField] private LEDBehaviour led;
    [SerializeField] private float leverAngle;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 finalPosition;

    void Start()
    {
        originalPosition = transform.position;
        finalPosition = originalPosition + moveDirection.normalized * moveDistance;
        targetPosition = transform.position;
    }

    void Update()
    {
        leverAngle = Vector3.Angle(leverHandle.up, transform.up);

        if (leverAngle >= leverAngleThreshold)
        {
            targetPosition = finalPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (leverAngle <= leverStartAngleThreshold)
        {
            targetPosition = originalPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        

        // Check if it's on target destination
        if (Vector3.Distance(finalPosition, transform.position) < positionThreshold)
            led.Toggle(true);
        else led.Toggle(false);
    }
}
