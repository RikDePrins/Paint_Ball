using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _spawnPoints = null;

    private int _currentJoinIndex = 0;

    public void OnJoin(PlayerInput input)
    {
        if(_currentJoinIndex >= _spawnPoints.Length) return;
        MeshRenderer[] meshRenderers = input.gameObject.GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = GameData.Instance.Materials[_currentJoinIndex];
        }
        FindFirstObjectByType<CinemachineTargetGroup>().AddMember(input.gameObject.transform, 1, 1);
        input.gameObject.transform.position = _spawnPoints[_currentJoinIndex];
        ++_currentJoinIndex;
    }

    public void RemoveFromCamera(Transform actor)
    {
        FindFirstObjectByType<CinemachineTargetGroup>().RemoveMember(actor);
    }
    //public void AddToCamera()
    //{
    //    FindFirstObjectByType<CinemachineTargetGroup>().AddMember(input.gameObject.transform, 1, 1);
    //}
}