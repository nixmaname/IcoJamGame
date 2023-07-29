using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public LimitedDictionary<int, Vector3> rec = new LimitedDictionary<int, Vector3>();
    public float remover;
    public int timer;
    public bool stop;

    private BoxCollider2D col;
    public int counter;

    GameObject leftWing, rightWing;


    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        leftWing = transform.GetChild(0).gameObject;
        rightWing = transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if (!stop)
        {
            //Replayvame ot Recorder
            timer++;
            if (rec.ContainsKey(timer))
            {
                transform.position = rec[timer];
                counter++;
            }
            if (timer >= rec.lastKey || counter >= rec.Count)
            {
                col.enabled = true;
                stop = true;
                leftWing.SetActive(true);
                rightWing.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        //Restartirame
        col.enabled = false;
        rec.Clear();
        timer = 0;
        stop = false;
        counter = 0;
        leftWing.SetActive(false);
        rightWing.SetActive(false);

    }
}
