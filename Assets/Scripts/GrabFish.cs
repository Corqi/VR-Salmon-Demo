using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections; 

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
[RequireComponent(typeof(Rigidbody))]
public class GrabbableFish : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody fishRigidbody;
    private Vector3 originalScale;
    
    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        fishRigidbody = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        
        // Configure grab settings
        grabInteractable.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
        grabInteractable.throwVelocityScale = 1f;
        grabInteractable.throwAngularVelocityScale = 0.5f;
        
        // Add event listeners
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Make conveyor belt ignore grabbed fish
        fishRigidbody.isKinematic = false;
        fishRigidbody.useGravity = true;
        
        // Optional: Slightly scale up when grabbed for better visibility
        transform.localScale = originalScale * 1.1f;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Return to original scale
        transform.localScale = originalScale;
        
        // Small delay before allowing conveyor to affect it again
        StartCoroutine(EnableConveyorInteraction());
    }

    private IEnumerator EnableConveyorInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        // Your existing conveyor belt script will handle movement
    }
}