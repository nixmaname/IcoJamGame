using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public Dictionary<int, Vector3> rec = new Dictionary<int, Vector3>();
    public float remover;
    public int timer;
    public bool stop;

    private BoxCollider2D col;
    public int counter;

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
                Debug.Log(timer);
            }
            if (counter >= rec.Count)
            {
                col.enabled = true;
                stop = true;
            }
        }
    }
    private void OnDisable()
    {
        col.enabled = false;
        rec.Clear();
        timer = 0;
        stop = false;
        counter = 0;
    }
}
