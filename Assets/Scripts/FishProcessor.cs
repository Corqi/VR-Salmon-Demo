using UnityEngine;
using System.Collections; 


public class FishProcessor : MonoBehaviour
{
    [Header("Pool Settings")]
    public FishPool fishPool;
    public string wholeFishTag = "WholeFish";
    public string guttedFishTag = "GuttedFish";
    
    [Header("Processing Settings")]
    public float processingTime = 2.0f;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            StartCoroutine(ProcessFish(other.gameObject));
        }
    }

    private IEnumerator ProcessFish(GameObject wholeFish)
    {
        // Get velocity before deactivating
        Rigidbody wholeRb = wholeFish.GetComponent<Rigidbody>();
        Vector3 velocity = wholeRb.linearVelocity;
        Vector3 angularVelocity = wholeRb.angularVelocity;

        // Return to pool
        fishPool.ReturnToPool(wholeFish);
        
        yield return new WaitForSeconds(processingTime);
        
        // Spawn gutted fish
        GameObject guttedFish = fishPool.SpawnFromPool(
            guttedFishTag, 
            spawnPoint.position, 
            spawnPoint.rotation
        );

        // Apply previous velocity
        Rigidbody guttedRb = guttedFish.GetComponent<Rigidbody>();
        guttedRb.linearVelocity = velocity;
        guttedRb.angularVelocity = angularVelocity;
    }
}