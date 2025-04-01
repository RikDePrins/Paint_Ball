using UnityEngine;
using UnityEngine.Events;

public class OrbBehaviour : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers = null;
    private MeshRenderer _otherMeshRenderer = null;
    private UnityEvent<Color> _onOrbEnterEvent = null;
    private UnityEvent<Color> _onOrbExitEvent = null;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _onOrbEnterEvent = GetComponentInParent<OrbManager>().onOrbEnterEvent;
        _onOrbExitEvent = GetComponentInParent<OrbManager>().onOrbExitEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_otherMeshRenderer != null)
        {
            _onOrbExitEvent?.Invoke(_otherMeshRenderer.material.color);
        }
        
        _otherMeshRenderer = other.GetComponentInChildren<MeshRenderer>();
        _onOrbEnterEvent?.Invoke(_otherMeshRenderer.material.color);

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material = _otherMeshRenderer.material;
        }
    }
}