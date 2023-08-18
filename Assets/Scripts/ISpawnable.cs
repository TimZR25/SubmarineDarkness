using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public Item GetItem();

    public void SetItemSpawner(ItemSpawner itemSpawner);
}
