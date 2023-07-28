using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public Dictionary<int, Vector3> rec = new Dictionary<int, Vector3>();
    public float remover;
    public int timer;
    bool stop;

    private BoxCollider2D col;
    int counter;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (!stop)
        {
            timer++;
            if (rec.ContainsKey(timer))
            {
                transform.position = rec[timer];
                counter++;
            }
            if (counter >= rec.Count)
            {
                col.enabled = true;
                stop = true;
                Debug.Log("JUMPY");
            }
        }
    }
    private void OnDisable()
    {
        rec.Clear();
        Debug.Log(rec.Count);
        timer = 0;
        stop = false;
    }
}
