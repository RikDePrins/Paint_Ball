using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody = null;

    [SerializeField]
    private float _movementForce = 5f;

    [SerializeField]
    private float _movementTorque = 5f;

    [SerializeField]
    bool _isTorqueEnabled = false;

    private Vector2 _normalizedBallMovementInput = Vector2.zero;
    private float _holdTime = 0;
    private bool _isDashing = false;
    private float _chargeRate = 1f;
    private float _maxDashForce = 10f;
    private float _dashForce = 0;

    public void Awake()
    {
        _maxDashForce = 75 * _movementForce;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _normalizedBallMovementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_rigidBody != null);
        if (_isDashing) return;
        if (_isTorqueEnabled)
        {
            Vector3 ballMovementDirection = new Vector3(_normalizedBallMovementInput.y, 0f, -_normalizedBallMovementInput.x);
            _rigidBody.AddTorque(_movementTorque * ballMovementDirection, ForceMode.Force);
        }
        else
        {
            Vector3 ballMovementDirection = new Vector3(_normalizedBallMovementInput.x, 0f, _normalizedBallMovementInput.y);
            _rigidBody.AddForce(_movementForce * ballMovementDirection, ForceMode.Force);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isDashing = true;
            _holdTime = 0; // Reset hold time when dash starts
        }
        else if (context.performed)
        {
            _holdTime += Time.deltaTime;
            _holdTime = Mathf.Clamp(_holdTime, 0, 1);
            _dashForce = _maxDashForce * _holdTime;
            Debug.Log(_dashForce);
        }
        else if (context.canceled)
        {
            Vector3 dashDirection = new Vector3(_normalizedBallMovementInput.x, 0f, _normalizedBallMovementInput.y);
            Vector3 torqueDirection = new Vector3(_normalizedBallMovementInput.y, 0f, -_normalizedBallMovementInput.x);

            Debug.Log($"Applying Dash: {_dashForce}");
            _rigidBody.AddForce(dashDirection * _dashForce, ForceMode.Impulse);
            _rigidBody.AddTorque(100 * torqueDirection, ForceMode.Force);

            // Reset state
            _holdTime = 0;
            _dashForce = 0;
            _isDashing = false;
        }
    }
}