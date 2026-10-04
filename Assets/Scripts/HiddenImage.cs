using UnityEngine;

public class HiddenImage : MonoBehaviour
{   
    [SerializeField] private GameObject hiddenImages;
    private void OnEnable()
    {
        StateMachine.Instance.OnStateChanged += HandleStateChanged;
    }
    private void OnDisable()
    {
        StateMachine.Instance.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        Debug.Log($"State changed to: {state} hidden images");
        if (state == GameState.Preview)
        {
            hiddenImages.SetActive(false);
        }
        if (state == GameState.Results && MemoryGameMachine.Instance.GetWinningStatus())
        {
            hiddenImages.SetActive(true);
            
        }
    }
}