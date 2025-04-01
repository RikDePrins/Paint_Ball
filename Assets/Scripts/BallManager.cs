using UnityEngine;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private Material[] _materials = null;

    private int _currentJoinIndex = 0;

    public void OnJoin(PlayerInput input)
    {
        MeshRenderer[] meshRenderers = input.gameObject.GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = _materials[_currentJoinIndex];
        }

        ++_currentJoinIndex;
    }

    public void OnLeave(PlayerInput input)
    {
        --_currentJoinIndex;
    }
}