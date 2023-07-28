using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    Vector3 oldpos;
    public CopyCat cc;
    public int timer;
    // Start is called before the first frame update
    void Start()
    {
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer ++;
        if (transform.position != oldpos && cc != null)
        {
            cc.rec.Add(timer, transform.position);
            oldpos = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F) && cc != null)
        {
            cc.gameObject.SetActive(true);
            cc = null;
            timer = 0;
            oldpos = transform.position;
        }
    }
}
