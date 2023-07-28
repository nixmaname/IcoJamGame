using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public Dictionary<int, Vector3> rec = new Dictionary<int, Vector3>();
    public float remover;
    public int timer;
    bool stop;

    private void Update()
    {
        if (!stop)
        {
            timer++;
            if (rec.ContainsKey(timer))
            {
                transform.position = rec[timer];
            }
        }

        if(transform.position == rec[rec.Count])
        {
            stop = true;
            gameObject.tag = "Untagged";
        }
    }
}
