using UnityEngine;

public class ToggleScreen : MonoBehaviour
{
    private Renderer screenRenderer;
    private Material screenMaterial;
    private RenderTexture renderTexture;
    private bool isOn = true;

    private Texture2D blackTexture;

    private void Start()
    {
        screenRenderer = GetComponent<Renderer>();

        screenMaterial = screenRenderer.material;
        renderTexture = screenMaterial.mainTexture as RenderTexture;

        // Create a black texture
        blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        blackTexture.Apply();

        // start with turned off screen
        Toggle();
    }

    public void Toggle()
    {
        if (isOn)
        {
            screenMaterial.SetTexture("_MainTex", blackTexture);
        }
        else
        {
            screenMaterial.SetTexture("_MainTex", renderTexture);
        }

        isOn = !isOn;
    }
}
