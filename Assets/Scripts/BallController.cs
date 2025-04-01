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

    public void OnMove(InputAction.CallbackContext context)
    {
        _normalizedBallMovementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_rigidBody != null);

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
            // Start tracking hold time
            _holdTime = 0f;
            _isDashing = true;
            _rigidBody.linearVelocity = Vector3.zero;  // Stop movement
        }
        else if (context.performed)
        {
            // Keep increasing hold time while button is held
            _holdTime += Time.deltaTime * _chargeRate;
            _holdTime = Mathf.Clamp(_holdTime, 0, _maxDashForce);
        }
        else if (context.canceled)
        {
            // Apply dash force in the forward direction
            Vector3 dashDirection = transform.forward;
            _rigidBody.AddForce(dashDirection * _holdTime, ForceMode.Impulse);

            // Reset state
            _isDashing = false;
            _holdTime = 0f;
        }
    }
}