using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody = null;

    [SerializeField]
    private float _movementForce = 5f;

    [SerializeField]
    private float _movementTorque = 5f;

    [SerializeField]
    private bool _isTorqueEnabled = false;

    private Vector2 _normalizedBallMovementInput = Vector2.zero;
    private float _holdTime = 0;
    private bool _isDashing = false;
    private float _chargeRate = 5f;
    private float _maxDashForce = 10f;

    public void OnMove(InputAction.CallbackContext context)
    {
        _normalizedBallMovementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_rigidBody != null);

        // Disable movement while dashing
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
            _holdTime = 0f;
            _isDashing = true;
        }
        else if (context.performed)
        {
            _holdTime += Time.deltaTime * _chargeRate;
            _holdTime = Mathf.Clamp(_holdTime, 0, _maxDashForce);
        }
        else if (context.canceled)
        {
            // Determine dash direction based on movement input or default to forward
            Vector3 dashDirection = new Vector3(_normalizedBallMovementInput.x, 0f, _normalizedBallMovementInput.y);

            //_rigidBody.AddTorque(dashDirection * 10000000, ForceMode.Force);
            _rigidBody.AddForce(dashDirection * 400, ForceMode.Force);
            // Reset dashing state
            _isDashing = false;
            _holdTime = 0f;
        }
    }
}