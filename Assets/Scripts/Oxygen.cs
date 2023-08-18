using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Oxygen : Item
{
    [SerializeField] private int _resource;
    [SerializeField] private Animator _animator;

    private ItemSpawner _itemSpawner;

    public override int GetResource() => _resource;


    public override void FollowHook(Transform target)
    {
        if (target == null) return;

        _animator.enabled = false;
        transform.position = target.position;
        transform.SetParent(target.transform);
    }


    public override void SetItemSpawner(ItemSpawner itemSpawner)
    {
        _itemSpawner = itemSpawner;
    }

    public override void RemoveItem()
    {
        _itemSpawner.RemoveItem();
        Destroy(gameObject);
    }
}
