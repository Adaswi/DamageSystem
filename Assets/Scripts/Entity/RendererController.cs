using UnityEngine;

public class RendererController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private UnityEngine.Rendering.ShadowCastingMode tempCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

    private void Awake()
    {
        GetChildRenderer();
    }

    public void GetChildRenderer()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void RenderShadowsOnly()
    {
        tempCastingMode = meshRenderer.shadowCastingMode;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

    public void RenderNoShadows()
    {
        tempCastingMode = meshRenderer.shadowCastingMode;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void RestoreCastingMode()
    {
        meshRenderer.shadowCastingMode = tempCastingMode;
    }
}
