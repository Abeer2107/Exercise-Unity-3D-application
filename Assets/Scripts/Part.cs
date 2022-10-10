using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private string displayName;
    [Tooltip("Part will be marked as connected & unmovable")]
    [SerializeField] private bool isBasePart;
    [Tooltip("Size of texture to be applied on part in case it has no texture")]
    [SerializeField] private Vector2Int textureSize = new Vector2Int(512, 512);

    private bool isConnected;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Rigidbody rb;
    private ColoringMode coloringMode;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();

        coloringMode = FindObjectOfType<ColoringMode>();
        if (coloringMode)
        {
            coloringMode.OnActivated.AddListener(OnColoringModeActivated);
            coloringMode.OnDeactivated.AddListener(OnColoringModeDeactivated);
        }

        //textureSize = new Vector2(meshRenderer.material.mainTexture.width, meshRenderer.material.mainTexture.height);

        if (isBasePart)
            isConnected = true;
    }

    private void OnDisable()
    {
        if (coloringMode)
        {
            coloringMode.OnActivated.RemoveListener(OnColoringModeActivated);
            coloringMode.OnDeactivated.RemoveListener(OnColoringModeDeactivated);
        }
    }

    #region Callbacks
    private void OnColoringModeActivated()
    {
        if(meshCollider)
            meshCollider.convex = false;
    }

    private void OnColoringModeDeactivated()
    {
        if (meshCollider)
            meshCollider.convex = true;
    }
    #endregion

    public void SetConnected(bool state)
    {
        isConnected = state;
    }

    public bool IsConnected()
    {
        return isConnected;
    }

    public MeshRenderer GetMeshRenderer()
    {
        return meshRenderer;
    }

    public Vector2Int GetTextureSize()
    {
        return textureSize;
    }
}
