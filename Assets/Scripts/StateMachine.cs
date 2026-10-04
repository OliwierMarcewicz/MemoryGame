using UnityEngine;

public class StateMachine : MonoSingleton<StateMachine>
{
    public GameState CurrentState { get; private set; }
    public event System.Action<GameState> OnStateChanged;

    private void Start()
    {
        SetState(GameState.Preview);
    }

    public void SetState(GameState state)
    {
        if (CurrentState == state)
            return;
        CurrentState = state;
        Debug.Log($"State changed to {state}");
        OnStateChanged?.Invoke(state);
    }
}
