using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private float _speed;
    public int GetScore() => _score;

    private bool IsReachedTarget => Vector3.Distance(transform.position, _currentTarget) <= 0.1;

    private Vector3 _currentTarget;

    private FishSpawner _fishSpawner;

    private bool CanMove = true;

    private void Start()
    {
        _currentTarget = _fishSpawner.GetRandomPoint;
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
            _currentTarget = _fishSpawner.GetRandomPoint;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

            if ((_currentTarget - transform.position).x < 0) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void FollowHook(Transform target)
    {
        if (target == null) return;

        CanMove = false;
        transform.position = target.position;
        transform.SetParent(target.transform);
    }

    public void SetFishSpawner(FishSpawner fishSpawner)
    {
        _fishSpawner = fishSpawner;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
