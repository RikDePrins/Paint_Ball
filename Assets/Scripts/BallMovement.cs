using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody = null;

    [SerializeField]
    private float _movementForce = 5f;

    [SerializeField]
    private float _movementTorque = 5f;

    [SerializeField]
    bool _isTorqueEnabled = false;

    [SerializeField]
    private Input _input = null;

    private void FixedUpdate()
    {
        Debug.Assert(_rigidBody != null);
        Debug.Assert(_input != null);

        Vector2 normalizedBallMovementInput = _input.GetNormalizedBallMovementInput();

        if (_isTorqueEnabled)
        {
            Vector3 ballMovementDirection = new Vector3(normalizedBallMovementInput.y, 0f, -normalizedBallMovementInput.x);
            _rigidBody.AddTorque(_movementTorque * ballMovementDirection, ForceMode.Force);
        }
        else
        {
            Vector3 ballMovementDirection = new Vector3(normalizedBallMovementInput.x, 0f, normalizedBallMovementInput.y);
            _rigidBody.AddForce(_movementForce * ballMovementDirection, ForceMode.Force);
        }
    }
}