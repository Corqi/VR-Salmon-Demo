using UnityEngine;

public class PlaneCatcher : MonoBehaviour
{
    [Header("Catcher Settings")]
    public bool isFinishedEating = false;    // Flag to decide whether to decrement counter
    public int counter = 10;          // The counter value that gets decremented

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinishedEating)
        {
            if (other.CompareTag("Pellet"))
            {
                counter--;
                Destroy(other.gameObject);

                if (counter <= 0)
                {
                    Collider collider = GetComponent<Collider>();
                    collider.enabled = false;

                    isFinishedEating = true;
                }
            }
        }
    }
}