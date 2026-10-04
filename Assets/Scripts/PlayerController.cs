using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f;

    private bool canMove = false;
    private Rigidbody rb;
    [SerializeField] private InputActionReference moveActionUp;
    [SerializeField] private InputActionReference moveActionDown;
    [SerializeField] private InputActionReference moveActionLeft;
    [SerializeField] private InputActionReference moveActionRight;

    private void OnEnable()
    {
        StateMachine.Instance.OnStateChanged += HandleStateChanged;

        moveActionUp.action.Enable();
        moveActionDown.action.Enable();
        moveActionLeft.action.Enable();
        moveActionRight.action.Enable();
    }
    private void OnDisable()
    {
        StateMachine.Instance.OnStateChanged -= HandleStateChanged;

        moveActionUp.action.Disable();
        moveActionDown.action.Disable();
        moveActionLeft.action.Disable();
        moveActionRight.action.Disable();
    }

    private void HandleStateChanged(GameState state)
    {
        canMove = state == GameState.Playing;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }   

    void Update()
    {
        if (!canMove) return;
        if (moveActionUp.action.IsPressed())
        {
            Move(0f, 1f);
        }
        if (moveActionDown.action.IsPressed())
        {
            Move(0f, -1f);
        }
        if (moveActionLeft.action.IsPressed())
        {
            Move(-1f, 0f);
        }
        if (moveActionRight.action.IsPressed())
        {
            Move(1f, 0f);
        }
    }
    private void Move(float x, float y)
    {
        if(Physics.Raycast(transform.position, new Vector3(x,0f,y), 0.5f))
        {
            return;
        }
        Vector3 moveDirection = new Vector3(x, 0f, y).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
