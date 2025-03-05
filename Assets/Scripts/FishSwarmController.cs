using UnityEngine;

public class FishSwarmController : MonoBehaviour
{
    [Header("Fish Rotation Settings")]
    public float minRadius = 3f;
    public float maxRadius = 7f;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private Transform[] fishes;
    private float[] speeds;
    private float[] heights;
    private float[] radii;

    void Start()
    {
        // Get all fish children
        fishes = new Transform[transform.childCount];
        speeds = new float[transform.childCount];
        heights = new float[transform.childCount];
        radii = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            fishes[i] = transform.GetChild(i);
            speeds[i] = Random.Range(minSpeed, maxSpeed);
            heights[i] = Random.Range(minHeight, maxHeight);
            radii[i] = Random.Range(minRadius, maxRadius);
        }
    }

    void Update()
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            if (fishes[i] == null) continue;

            float angle = Time.time * speeds[i];
            Vector3 newPosition = new Vector3(Mathf.Cos(angle) * radii[i], heights[i], Mathf.Sin(angle) * radii[i]);
            fishes[i].position = transform.position + newPosition;

            // Make fish face in the direction of movement
            fishes[i].LookAt(new Vector3(transform.position.x, fishes[i].position.y, transform.position.z));
            fishes[i].Rotate(0, -90, 0);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
