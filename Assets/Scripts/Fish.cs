using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    [SerializeField] private int _point;
    private Transform _target;

    public int GetPoint() => _point;

    private void FixedUpdate()
    {
        FollowTarget(_target);
    }

    public void FollowTarget(Transform target)
    {
        if (target == null) return;
        
        _target = target;

        transform.position = _target.position;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
