using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    [SerializeField] private Transform _hookPoint;
    [SerializeField] private float _hookRange;

    private Item _hookedItem;

    public Action<Item> ItemRemoved;

    public void TryHook()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_hookPoint.position, _hookRange);

        if (hitCollider == null) return;

        if (hitCollider.TryGetComponent(out Item item))
        {
            _hookedItem = item;

            _hookedItem.FollowHook(_hookPoint);
        }
    }

    public void TryRemoveItem()
    {
        if (_hookedItem == null) return;

        ItemRemoved?.Invoke(_hookedItem);
        _hookedItem.RemoveItem();
        _hookedItem = null;
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
