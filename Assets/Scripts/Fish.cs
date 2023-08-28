using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : Item
{
    [SerializeField] private int _resource;
    [SerializeField] private float _speed;
    public override int GetResource() => _resource;

    private bool IsReachedTarget => Vector3.Distance(transform.position, _currentTarget) <= 0.1;

    private Vector3 _currentTarget;

    private ItemSpawner _itemSpawner;

    private bool CanMove = true;

    private void Start()
    {
        _currentTarget = Points.GetRandomPoint;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (CanMove == false) return;

        if (IsReachedTarget == true)
        {
            _currentTarget = Points.GetRandomPoint;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

            if ((_currentTarget - transform.position).x < 0) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void FollowHook(Transform target)
    {
        if (target == null) return;

        CanMove = false;
        transform.position = target.position;
        transform.SetParent(target.transform);
    }

    public override void RemoveItem()
    {
        _itemSpawner.RemoveItem(this);
        Destroy(gameObject);
    }

    public override void SetItemSpawner(ItemSpawner itemSpawner)
    {
        _itemSpawner = itemSpawner;
    }
}
