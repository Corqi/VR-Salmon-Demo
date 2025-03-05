using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject foodPrefab;    // The food prefab to be instantiated
    public float spawnInterval = 5f; // Time interval (in seconds) between food spawns
    public bool isSpawning = false;  // Toggle spawning on/off
    public float shootForce = 10f;   // The force with which the food is shot
    public float randomizationFactor = 0.1f; // Small factor to randomize direction

    [Header("Target Object")]
    public Transform targetObject;   // The object towards which the food will be shot

    [SerializeField] private Transform leverHandle;
    [SerializeField] private float leverAngleThreshold = 45f;
    [SerializeField] private float leverAngle;
    [SerializeField] private PlaneCatcher planeCatcher;
    [SerializeField] private LEDBehaviour led;
    private bool ledToggled = false;

    private float nextSpawnTime = 0f;

    private Quaternion initialLeverRotation;

    void Start()
    {
        initialLeverRotation = leverHandle.rotation;
    }

    void Update()
    {
        leverAngle = Quaternion.Angle(initialLeverRotation, leverHandle.rotation);

        isSpawning = (leverAngle >= leverAngleThreshold) ? true : false;


        // If spawning is enabled and it's time to spawn
        if (isSpawning && Time.time >= nextSpawnTime)
        {
            SpawnFood();
            nextSpawnTime = Time.time + spawnInterval;  // Update next spawn time
        }

        // Check if feeding is done
        if (planeCatcher.isFinishedEating && ledToggled == false)
        {
            led.Toggle(true);
            ledToggled = true;
        }
    }

    // Function to spawn food and shoot it towards the target object
    void SpawnFood()
    {
        // Instantiate the food prefab at the cylinder's position
        GameObject spawnedFood = Instantiate(foodPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody component of the spawned food
        Rigidbody rb = spawnedFood.GetComponent<Rigidbody>();

        if (rb != null && targetObject != null)
        {
            // Get direction from spawn point to the target object
            Vector3 directionToTarget = (targetObject.position - transform.position).normalized;

            // Apply small randomization to the direction
            Vector3 randomOffset = new Vector3(
                Random.Range(-randomizationFactor, randomizationFactor),
                Random.Range(-randomizationFactor, randomizationFactor),
                Random.Range(-randomizationFactor, randomizationFactor)
            );

            // Add randomization to the direction
            directionToTarget += randomOffset;

            // Apply force to shoot the food towards the target object
            rb.AddForce(directionToTarget.normalized * shootForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Target Object is not assigned!");
        }
    }

    // Optional: You can use this function to toggle spawning from other scripts
    public void ToggleSpawning(bool state)
    {
        isSpawning = state;
    }
}
