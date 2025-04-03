using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.Events;
using Unity.Cinemachine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private GameObject _LandMineTemplate = null;
    [SerializeField]
    private float _Cooldown = 15f;
    private float _Timer = 0;

    [SerializeField]
    private GameObject _RollVFXTemplate = null;
    private GameObject _RollVFXObject = null;
    private VisualEffect _RollVFX = null;

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
    private float _maxDashForce = 10f;
    private List<AudioSource> _audioSources = new List<AudioSource>();
    private bool _startGame = false;
    private float _dashCoolDown = 3f;
    private float _knockbackCooldown = 0f;
    private Vector3 _startingPosition;
    private bool _isRespawning = false;
    private float _respawnTimer = 3f;
    private List<CinemachineImpulseSource> _cameraShake = new List<CinemachineImpulseSource>();
    private List<GameObject> _tilesInRadius = new List<GameObject>();

    public void Awake()
    {
        _maxDashForce = 75 * _movementForce;
        _audioSources = new List<AudioSource>(GetComponents<AudioSource>());
        _startingPosition = transform.position;
        _RollVFXObject = Instantiate(_RollVFXTemplate);
        _RollVFXObject.transform.parent = null;
        _cameraShake.AddRange(FindObjectsByType<CinemachineImpulseSource>(FindObjectsSortMode.None));

        _RollVFX = _RollVFXObject.GetComponent<VisualEffect>();
        _RollVFX.SetVector3("BasePosition", transform.position - new Vector3(0, 0.5f, 0));
    }
    public void Start()
    {
       
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _normalizedBallMovementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_rigidBody != null);
        if (!_startGame) return;
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


        _RollVFX.SetBool("Moving", _rigidBody.linearVelocity.magnitude > 1f);
        _RollVFX.SetVector3("VelocityDirection", _rigidBody.linearVelocity);
        _RollVFX.SetVector3("BasePosition", transform.position - new Vector3(0,0.5f,0));
    }
    private void OnDestroy()
    {
        if(_RollVFXObject) Destroy(_RollVFXObject);
    }
    private void Update()
    {
        if (_knockbackCooldown > 0)
        {
            _knockbackCooldown -= Time.deltaTime;
        }
        _dashCoolDown -= Time.deltaTime;

        if (transform.position.y < -5 && !_isRespawning)
        {
            _isRespawning = true;
            _respawnTimer = 3f;
            _rigidBody.useGravity = false;
            _rigidBody.isKinematic = true;
            gameObject.transform.position = new Vector3(8, -4, 8);
            _audioSources[1].Play();
            _ = StartRespawn();
        }

        if (_Timer > 0f) _Timer -= Time.deltaTime;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (_dashCoolDown > 0 || !_startGame) return;

        if (context.started)
        {
            _isDashing = true;
        }
        else if (context.canceled)
        {
            if (!_isDashing) return;
            float dashForce = _maxDashForce * 2 * (Mathf.Clamp(_holdTime, 0f, 1f) / 1f);
            Vector3 dashDirection = new Vector3(_normalizedBallMovementInput.x, 0f, _normalizedBallMovementInput.y);
            Vector3 torqueDirection = new Vector3(_normalizedBallMovementInput.y, 0f, -_normalizedBallMovementInput.x);

            _rigidBody.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            _rigidBody.AddTorque(100 * torqueDirection, ForceMode.Impulse);
            _audioSources[0].Play();

            _knockbackCooldown = 0.5f;
            _dashCoolDown = 3f;
            _isDashing = false;
        }
        else if (context.performed)
        {
            if (!_isDashing) return;
            _holdTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_knockbackCooldown > 0 && collision.gameObject.CompareTag("Player"))
        {
            Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRigidBody != null)
            {
                otherRigidBody.gameObject.GetComponent<BallController>().ExplosionEffect(this.gameObject.GetComponentInChildren<MeshRenderer>().material.color);
                Vector3 launchDirection = (collision.transform.position - transform.position).normalized;
                otherRigidBody.AddForce(launchDirection * (_maxDashForce / 50), ForceMode.Impulse);
                _cameraShake.ForEach(x => x.GenerateImpulse());
                _rigidBody.linearVelocity = Vector3.zero;
                _rigidBody.angularVelocity = Vector3.zero;
            }
        }
    }

    public void StartGame()
    {
        _startGame = true;
        
    }

    public void AddTileInRadius(GameObject tile)
    {
        _tilesInRadius.Add(tile);
    }

    public void RemoveTileInRadius(GameObject tile)
    {
        _tilesInRadius.Remove(tile);
    }

    public void ExplosionEffect(Color color)
    {
        foreach (GameObject tile in _tilesInRadius)
        { 
            tile.GetComponent<TileBehaviour>().SetColor(color);
        }
    }

    private async Task StartRespawn()
    {
        await Task.Delay(TimeSpan.FromSeconds(_respawnTimer));
        transform.position = _startingPosition;
        _rigidBody.useGravity = true;
        _rigidBody.isKinematic = false;
        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _isRespawning = false;
    }

    public void OnPlantMine(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            if (_Timer > 0f) return;
            var mineObject = Instantiate(_LandMineTemplate, transform.position, Quaternion.identity);
            mineObject.transform.parent = null;

            mineObject.GetComponent<LandMineBehaviour>().OwningPlayer = gameObject;
            _Timer = _Cooldown;
        }
       
    }
}