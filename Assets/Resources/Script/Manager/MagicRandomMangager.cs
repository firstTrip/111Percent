using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRandomMangager<T>
{
    private struct Entry
    {
        public T Item;
        public float AccumulatedWeight;
    }

    private List<Entry> entries = new List<Entry>();
    private float totalWeight = 0;

    public void AddEntry(T item, float weight)
    {
        if (weight <= 0) return;

        totalWeight += weight;
        entries.Add(new Entry { Item = item, AccumulatedWeight = totalWeight });
    }

    public T PickRandom()
    {
        if (entries.Count == 0)
            throw new InvalidOperationException("목록이 비어 있습니다!");

        float r = UnityEngine.Random.Range(0, totalWeight);

        foreach (var entry in entries)
        {
            if (r < entry.AccumulatedWeight)
                return entry.Item;
        }

        return default;
    }
}
