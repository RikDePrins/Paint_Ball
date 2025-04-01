using UnityEngine;

public class Input : MonoBehaviour
{
    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Ball.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Ball.Disable();
        _inputActions.Dispose();
    }

    public Vector2 GetNormalizedBallMovementInput()
    {
        return _inputActions.Ball.Move.ReadValue<Vector2>().normalized;
    }
}