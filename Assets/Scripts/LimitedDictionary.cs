using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LimitedDictionary<TKey, TValue>
{
    //Toq class limitira razmera na dictionaryto kato premahva pyrvoto zapisano ako razmera na dictionarito e nad maxSize

    private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(); // dictionaryto
    private readonly Queue<TKey> insertionOrder = new Queue<TKey>(); // Queue da sverqva ordera na dictionaryto 
    private int maxSize = 400; // V recorder scripta  cc.timer = timer - x; kydeto x e maxSize
    private TKey oldestKey;
    public TKey lastKey; 

    public int MaxSize
    {
        get => maxSize;
        set
        {
            maxSize = value;
            while (dictionary.Count > maxSize)
            {
                TKey oldestKey = insertionOrder.Dequeue();
                dictionary.Remove(oldestKey);
            }
        }
    }

    public int Count => dictionary.Count;

    public TValue this[TKey key]
    {
        get => dictionary[key];
        set => Add(key, value);
    }

    public TKey OldestKey => oldestKey;

    public TValue OldestValue => dictionary[oldestKey];

    public TKey LastKey => lastKey;

    public TValue LastValue => dictionary[lastKey];

    public void Add(TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value; // If the key already exists, update its value
        }
        else
        {
            dictionary.Add(key, value);
            insertionOrder.Enqueue(key);

            if (dictionary.Count > maxSize)
            {
                oldestKey = insertionOrder.Dequeue();
                dictionary.Remove(oldestKey);
            }

            lastKey = key; // Update the last key
        }
    }

    public bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return dictionary.TryGetValue(key, out value);
    }

    public bool Remove(TKey key) // Ne raboti za sega
    {
        if (dictionary.Remove(key))
        {
            //insertionOrder.Remove(key);

            if (oldestKey.Equals(key))
                oldestKey = insertionOrder.Count > 0 ? insertionOrder.Peek() : default;

            if (lastKey.Equals(key))
                lastKey = insertionOrder.Count > 0 ? insertionOrder.ToArray()[insertionOrder.Count - 1] : default;

            return true;
        }
        return false;
    }

    public void Clear()
    {
        dictionary.Clear();
        insertionOrder.Clear();
        oldestKey = default;
        lastKey = default;
    }
}
