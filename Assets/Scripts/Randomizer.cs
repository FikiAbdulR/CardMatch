using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Shuffle")]
    public void Shuffle()
    {
        List<int> indexes = new List<int>();
        List<Transform> items = new List<Transform>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            indexes.Add(i);
            items.Add(transform.GetChild(i));
        }

        foreach (var item in items)
        {
            item.SetSiblingIndex(indexes[Random.Range(0, indexes.Count)]);
        }
    }
}
