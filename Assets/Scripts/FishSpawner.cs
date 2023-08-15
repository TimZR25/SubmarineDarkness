using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Manipulator _manipulator;

    [Header("SpawnSettings")]
    [SerializeField] private Fish _fishPrefab;
    [SerializeField] private int _maxFishes;
    [SerializeField] private float _delayBetweenSpawn;

    [Header("GridSettings")]
    [SerializeField] private int _length;
    [SerializeField] private int _wide;

    private int[] _pointsX;
    private int[] _pointsY;

    public int FishCount { get; private set; }

    public Vector3 GetRandomPoint => new Vector3(_pointsX[Random.Range(0, _pointsX.Length)], _pointsY[Random.Range(0, _pointsY.Length)], 0);


    private void Awake()
    {
        SetPoints();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _delayBetweenSpawn);
    }

    private void SetPoints()
    {
        _pointsX = new int[_length];
        _pointsY = new int[_wide];

        for (int i = 0; i < _length; i++)
        {
            _pointsX[i] = -_length / 2 + i;
        }

        for (int i = 0; i < _wide; i++)
        {
            _pointsY[i] = -_wide / 2 + i;
        }
    }

    private void Spawn()
    {
        if (FishCount >= _maxFishes) return;

        Fish fish = Instantiate(_fishPrefab, GetRandomPoint, Quaternion.identity, transform);
        fish.SetFishSpawner(this);

        FishCount++;
    }
    
    private void RemoveFish(int amount)
    {
        FishCount--;
    }

    private void OnEnable()
    {
        _manipulator.FishSold += RemoveFish;
    }

    private void OnDisable()
    {
        _manipulator.FishSold -= RemoveFish;
    }
}
