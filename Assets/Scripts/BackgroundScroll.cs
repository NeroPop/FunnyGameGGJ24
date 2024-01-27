using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    private float _scrollSpeed = 0;
    public Vector2 offset;
    private Material material;
    private CanvasRenderer crenderer;
    public bool componentsGotten = false;

    // Start is called before the first frame update
    void Start()
    {
        crenderer = gameObject.GetComponent<CanvasRenderer>();
        Debug.Log(crenderer);
    }
    // Update is called once per frame
    void Update()
    {
        if (!componentsGotten)
        { 
            material = crenderer.GetMaterial();
            Debug.Log(material.GetTextureOffset("_MainTex"));
            material.SetTextureOffset("_MainTex", new Vector2(0, 0));

            componentsGotten = true;
        }
        _scrollSpeed += (scrollSpeed * Time.deltaTime);

        offset = new Vector2(_scrollSpeed, -_scrollSpeed);
        material.SetTextureOffset("_MainTex", offset);

    }
}
