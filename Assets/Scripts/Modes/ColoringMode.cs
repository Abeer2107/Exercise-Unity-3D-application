using UnityEngine;

public class ColoringMode : Mode
{
    [SerializeField] private Texture2D brushTex;

    private Ray ray;
    private RaycastHit hit;
    private Color32[] brushPixels;
    private Texture2D tex;
    private Color currColor;
    private Part currPart;

    protected override void Start()
    {
        base.Start();

        brushPixels = brushTex.GetPixels32();
        currColor = Color.blue;
    }

    protected override void OnClickDown()
    {
        base.OnClickDown();

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<Part>() is Part part)
            {
                currPart = part;
                TargetTexture(hit, part);

                if (tex)
                    Draw(hit.textureCoord);
            }
        }
    }

    protected override void OnDrag()
    {
        base.OnDrag();

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<Part>() is Part part)
            {
                if(part != currPart)
                {
                    tex = null;
                    currPart = part;
                    TargetTexture(hit, part);
                }

                if (tex)
                    Draw(hit.textureCoord);
            }
        }
    }

    protected override void OnClickUp()
    {
        base.OnClickUp();

        tex = null;
    }

    private void TargetTexture(RaycastHit hit, Part part)
    {
        MeshRenderer meshRenderer = currPart.GetMeshRenderer();
        Mesh mesh = hit.collider.gameObject.GetComponent<MeshFilter>().sharedMesh;
        int matIndex = GetSubmeshIndex(hit.triangleIndex, mesh);
        //Debug.Log(matIndex);

        if (!tex)
        {
            tex = meshRenderer.materials[matIndex].mainTexture as Texture2D;
            //tex = meshRenderer.material.mainTexture as Texture2D;
        }

        if (!tex)
        { //Create texture & apply it
            tex = new Texture2D(part.GetTextureSize().x, part.GetTextureSize().y);
            Color[] pixels = tex.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = Color.white;

            tex.SetPixels(pixels);
            tex.Apply();

            meshRenderer.materials[matIndex].mainTexture = tex;
            //meshRenderer.material.mainTexture = tex;
            tex = meshRenderer.materials[matIndex].mainTexture as Texture2D;
            //tex = meshRenderer.material.mainTexture as Texture2D;
        }
    }

    private int GetSubmeshIndex(int triangleIndex, Mesh mesh)
    {
        int triangleCount = 0;
        for (int i = 0; i < mesh.subMeshCount; ++i)
        {
            var triangles = mesh.GetTriangles(i);
            triangleCount += triangles.Length / 3;
            if (triangleIndex < triangleCount) return i;
        }
        return 0;
    }

    public void Draw(Vector2 textureCoord)
    {
        Vector2Int pixelCoord = new Vector2Int((int)(textureCoord.x * tex.width), (int)(textureCoord.y * tex.height));
        Vector2Int lowerLeftCorner = new Vector2Int((int)(pixelCoord.x - brushTex.width / 2f), (int)(pixelCoord.y - brushTex.height / 2f));

        for (int i = 0, x, y; i < brushPixels.Length; i++)
        {
            x = i % brushTex.width;
            y = i / brushTex.width;

            float pixelAlpha = brushPixels[i].a / 255f;
            if (pixelAlpha > 0)
            {
                currColor.a = pixelAlpha;
                Vector2Int pixel = new Vector2Int(lowerLeftCorner.x + x, lowerLeftCorner.y + y);

                if (pixel.x >= 0 && pixel.x < tex.width && pixel.y >= 0 && pixel.y < tex.height)
                {
                    Color baseColor = tex.GetPixel(pixel.x, pixel.y);
                    Color finalColor = Color.Lerp(baseColor, currColor, currColor.a);
                    finalColor.a = currColor.a + baseColor.a;
                    tex.SetPixel(pixel.x, pixel.y, finalColor);
                }
            }
        }

        tex.Apply();
    }
}
