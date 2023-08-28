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

    public List<Item> Items;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _delayBetweenSpawn);
    }


    private void Spawn()
    {
        if (Items.Count >= _maxItems) return;

        Item item = Instantiate(_itemPrefab, Points.GetRandomPoint, Quaternion.identity, transform);
        Items.Add(item);
        item.SetItemSpawner(this);
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
}