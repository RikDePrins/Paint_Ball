using Unity.Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Cinemachinemanager : MonoBehaviour
{
    [SerializeField]
    private CinemachineTargetGroup _targetGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<CinemachineTargetGroup.Target> targets = _targetGroup.Targets;
        foreach (var target in targets)
        {
            
        }

    }

    void AddTarget(Transform target, float weight, float radius)
    {
        _targetGroup.AddMember(target, weight, radius);
    }
}
