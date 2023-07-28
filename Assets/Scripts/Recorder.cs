using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Vector3 oldpos;
    public CopyCat cc;
    public int timer;
    public bool canPress = true;
    public bool canRecord = true;

    Dictionary<int, string> test = new Dictionary<int, string>(); 
    // Start is called before the first frame update
    void Start()
    {
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (cc != null && cc.rec.Count > 200)
        {
            cc.rec.Remove(cc.rec.Keys.First());
        }
        if (transform.position != oldpos && cc != null && canRecord)
        {
            cc.rec.Add(timer, transform.position);
            oldpos = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F) && cc != null && canPress)
        {
            cc.gameObject.SetActive(true);
            cc.timer = cc.rec.Keys.First();
            cc.counter = cc.timer;
            Debug.Log(cc.rec.Keys.First());
            cc = null;
            canPress = false;
            canRecord = false;
        }
    }
}
