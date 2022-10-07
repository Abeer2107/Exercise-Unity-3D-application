using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private bool isBasePart;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(512, 512);

    private bool isConnected;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //textureSize = new Vector2(meshRenderer.material.mainTexture.width, meshRenderer.material.mainTexture.height);

        if (isBasePart)
            isConnected = true;
    }

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
