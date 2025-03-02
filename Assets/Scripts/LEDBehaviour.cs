using TMPro;
using UnityEngine;

public class LEDBehaviour : MonoBehaviour
{
    [SerializeField] private bool isOn;

    [SerializeField] private Color offColor = Color.black;
    [SerializeField] private Color onColor = Color.green;
    [SerializeField] private float brightness = 2f;

    private Material ledMaterial;

    private void Start()
    {
        ledMaterial = GetComponent<Renderer>().material;
        ledMaterial.EnableKeyword("_EMISSION");

        Toggle(false);
    }

    public void Toggle(bool state)
    {
        isOn = state;
        ledMaterial.SetColor("_EmissionColor", isOn ? onColor * brightness : offColor);
    }
}
