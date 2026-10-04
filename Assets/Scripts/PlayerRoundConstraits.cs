using UnityEngine;

public class PlayerRoundConstraits : MonoBehaviour
{
    private int ColorId;
    [SerializeField] private Color color;

    private void Awake()
    {
        color = Color.wheat;
        GetComponent<Renderer>().material.color = color;
    }
    public void SetColorId(int id)
    {
        ColorId = id;
    }
    public void SetColor(Color newColor)
    {
        color = newColor;
        GetComponent<Renderer>().material.color = color;
    }
    public int GetColorId => ColorId;
}
