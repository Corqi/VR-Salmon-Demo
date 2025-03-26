using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1f;              // Speed of object movement
    public float textureSpeed = 0.5f;     // Speed of texture scrolling
    public Vector3 direction = Vector3.right; // Movement direction (now right)
    
    [Header("Runtime Data")]
    public List<GameObject> onBelt;       // Objects on belt
    
    private Material beltMaterial;
    private Vector2 textureOffset;
    private Vector2 textureScale = new Vector2(3f, 1f); // Stretch texture horizontally

    void Start()
    {
        // Get material and configure tiling
        beltMaterial = GetComponent<MeshRenderer>().material;
        beltMaterial.mainTextureScale = textureScale; // Stretch texture
        textureOffset = Vector2.zero;
    }

    void Update()
    {
        // Scroll texture horizontally (X axis instead of Y)
        textureOffset.x += textureSpeed * Time.deltaTime;
        beltMaterial.mainTextureOffset = textureOffset;
    }

    void FixedUpdate()
    {
        // Move objects (unchanged from your working version)
        for(int i = 0; i < onBelt.Count; i++)
        {
            Rigidbody rb = onBelt[i]?.GetComponent<Rigidbody>();
            if(rb != null) rb.linearVelocity = speed * direction;
            else { onBelt.RemoveAt(i); i--; }
        }
    }

    void OnCollisionEnter(Collision collision) => AddToBelt(collision.gameObject);
    void OnCollisionExit(Collision collision) => onBelt.Remove(collision.gameObject);
    
    void AddToBelt(GameObject obj)
    {
        if(!onBelt.Contains(obj)) onBelt.Add(obj);
    }
}