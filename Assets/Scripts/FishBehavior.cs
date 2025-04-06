using UnityEngine;

public class FishBehavior : MonoBehaviour, IPooledFish
{
    public void OnFishSpawn()
    {
        // Reset any fish-specific states here
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}