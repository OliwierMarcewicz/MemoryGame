using UnityEngine;
using UnityEngine.InputSystem;

public class MemoryGameMachine : MonoSingleton<MemoryGameMachine>
{
    private bool areYouWinning;
    private void Awake()
    {
        areYouWinning = false;
        Application.targetFrameRate = 144;

    }
    private void Start()
    {
        
        StartRound();
    }

    private void StartRound()
    {
        StateMachine.Instance.SetState(GameState.Preview);
        areYouWinning = false;
    }
    public void StartGame()
    {
        StateMachine.Instance.SetState(GameState.Playing);
        
    }
    public void EndGame()
    {
        StateMachine.Instance.SetState(GameState.Results);
    }
    public void ResetGame()
    {
        StateMachine.Instance.SetState(GameState.Preview);
        areYouWinning = false;
    }
    public bool GetWinningStatus()
    {
        return areYouWinning;
    }
    public void SetWinningStatus(bool status)
    {
        areYouWinning = status;
    }
}
