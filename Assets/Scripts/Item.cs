using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void SetItemSpawner(ItemSpawner itemSpawner);

    public abstract void FollowHook(Transform target);

    public abstract int GetResource();

    public abstract void RemoveItem();
}

public enum ItemType
{
    Fish, BigFish, Oxygen
}
