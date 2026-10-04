using UnityEngine;

public class MemoryTile : MonoBehaviour
{
    [SerializeField] private Renderer tileRenderer;

    private int id;
    public Color TileColor { get; private set; }
    private void Awake()
    {
        if (tileRenderer == null)
        {
            tileRenderer = GetComponent<Renderer>();
        }
    }

    public void SetColor(Color color)
    {
        TileColor = color;
        tileRenderer.material.color = color;
    }
    public void HideColor()
    {
        tileRenderer.material.color = Color.gray; // Hide the color by setting it to gray
    }
    public void RevealColor()
    {
        tileRenderer.material.color = TileColor; // Reveal the original color
    }
    public void SetId(int id)
    {
        this.id = id;
    }
    public int Id => id;
}
