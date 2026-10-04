using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference resetAction;
    private PlayerRoundConstraits playerRoundConstraits;
    private void OnEnable()
    {
        interactAction.action.Enable();
        resetAction.action.Enable();
    }
    private void OnDisable()
    {
        interactAction.action.Disable();
        resetAction.action.Disable();
    }
    private void Awake()
    {
        playerRoundConstraits = FindFirstObjectByType<PlayerRoundConstraits>();
    }

    private void Update()
    {
        if (interactAction.action.WasPressedThisFrame())
        {
            Debug.Log("Interact action pressed");
            TryInteract();
            
        }

        if (resetAction.action.WasPressedThisFrame())
        {
            Debug.Log("Reset action pressed");
            MemoryGameMachine.Instance.ResetGame();
        }
    }
    private void TryInteract()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            MemoryTile interactable = hit.collider.GetComponent<MemoryTile>();
            if (interactable != null)
            {
                interactable.RevealColor();
                if (interactable.Id == playerRoundConstraits.GetColorId)
                {
                    Debug.Log("Correct tile selected!");
                    ScoreManager.Instance.AddScore(1);
                }
                else
                {
                    Debug.Log("Incorrect tile selected!");
                    MemoryGameMachine.Instance.EndGame();
                }
            }
        }
    }
}
