using UnityEngine;

public class GroundMaterial : MonoBehaviour
{
    [SerializeField] private Color groundColor;
    private Color originalColor;
    private void OnEnable()
    {
        StateMachine.Instance.OnStateChanged += HandleStateChanged;

    }
    private void OnDisable()
    {
        StateMachine.Instance.OnStateChanged -= HandleStateChanged;
    }
    private void Awake()
    {
        GetComponent<Renderer>().material.color = groundColor;
        originalColor = groundColor;
    }
    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Preview)
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }
    public void SetGroundColor(Color newColor)
    {
        groundColor = newColor;
        GetComponent<Renderer>().material.color = groundColor;
    }
}
