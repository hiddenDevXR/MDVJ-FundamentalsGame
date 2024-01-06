using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingTest : MonoBehaviour
{
    public RecollectorSystem recollectorSystem;
    public List<Item> itemPool;
    private int index = 0;

    private void Awake()
    {
        itemPool = new List<Item>();
        Item[] items = gameObject.GetComponentsInChildren<Item>();
        foreach (Item item in items)
        {
            itemPool.Add(item);
            item.gameObject.SetActive(false);
        }

        itemPool[index].gameObject.SetActive(true);

        recollectorSystem.OnReportCollected += ManagePool;
    }

    void ManagePool()
    {
        index++;
        if(index > itemPool.Count)
            index = 0;

        itemPool[index].gameObject.SetActive(true);
    }

    public void Unsuscribe()
    {
        recollectorSystem.OnReportCollected -= ManagePool;
    }
}
