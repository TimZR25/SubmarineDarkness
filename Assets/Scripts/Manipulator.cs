using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    [SerializeField] private Transform _hookPoint;
    [SerializeField] private float _hookRange;

    private Fish _hookedFish;

    public Action<int> FishSold;

    public void TryHook()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_hookPoint.position, _hookRange);

        if (hitCollider == null) return;

        if (hitCollider.TryGetComponent(out Fish fish))
        {
            _hookedFish = fish;

            _hookedFish.FollowHook(_hookPoint);
        }
    }

    public void TrySellFish()
    {
        if (_hookedFish == null) return;

        FishSold?.Invoke(_hookedFish.GetScore());
        _hookedFish.Die();
        _hookedFish = null;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_hookPoint.position, _hookRange);
    }
}
