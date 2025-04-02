using UnityEngine;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _spawnPoints = null;

    private int _currentJoinIndex = 0;

    public void OnJoin(PlayerInput input)
    {
        MeshRenderer[] meshRenderers = input.gameObject.GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = GameData.Instance.Materials[_currentJoinIndex];
        }

        input.gameObject.transform.position = _spawnPoints[_currentJoinIndex];
        ++_currentJoinIndex;
    }

    public void OnLeave(PlayerInput input)
    {
        --_currentJoinIndex;
    }
}