using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    private Camera mainCamera;

    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {       
        if(other.TryGetComponent(out Target _target))
        {
            targets.Add(_target);
            _target.OnDestroyed += RemoveTarget;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Target _target))
        {
            RemoveTarget(_target);
        }
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) return false;

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach(Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (viewPos.x < 0 || viewPos.x > 1) continue;
            if (viewPos.y < 0 || viewPos.y > 1) continue;

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) return false;

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1, 2);
        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) return;

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target _target)
    {
        if(CurrentTarget == _target)
        {
            cineTargetGroup.RemoveMember(_target.transform);
            CurrentTarget = null;
        }

        _target.OnDestroyed -= RemoveTarget;
        targets.Remove(_target);
    }
}
