using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Manipulator _manipulator;

    [Header("SpawnSettings")]
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private int _maxItems;
    [SerializeField] private float _delayBetweenSpawn;

    public int ItemCount { get; private set; }


    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _delayBetweenSpawn);
    }


    private void Spawn()
    {
        if (ItemCount >= _maxItems) return;

        Item item = Instantiate(_itemPrefab, Points.GetRandomPoint, Quaternion.identity, transform);
        item.SetItemSpawner(this);
        ItemCount++;
    }

    public void RemoveItem()
    {
        ItemCount--;
    }
}