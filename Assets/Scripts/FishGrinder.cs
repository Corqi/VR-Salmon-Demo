using UnityEngine;

public class FishGrinder : MonoBehaviour
{
    public ParticleSystem bloodEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            bloodEffect.Play();
            Destroy(other.gameObject);
        }
    }
}