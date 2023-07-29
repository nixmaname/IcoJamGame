using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    //Toq script zapisva poziciite na playera v limited dictionary v CopyCat

    public Vector3 oldpos;
    public CopyCat cc;
    public int timer;
    public bool canPress = true;
    public bool canRecord = true;

    int fps;
    float sec;
    int averageFps;

    // Start is called before the first frame update
    void Start()
    {
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (sec < 1)
        {
            sec += Time.deltaTime;
            fps++;
        }
        else
        {
            sec = 0;
            averageFps = fps*4;
            fps = 0;
            if (cc != null)
            {
                cc.rec.maxSize = averageFps;
            }
        }

        timer++;
       
        //Zapisvame poziciqta samo ako ima promqna v poziciite
        if (transform.position != oldpos && cc != null && canRecord)
        {
            cc.rec.Add(timer, transform.position);
            oldpos = transform.position;
        }
        //F - spawnvame duhcheto da povtori dvijeniqta i mahame 400 za da sverim timerite na Recorder i CopyCat
        // timerite slujat za tyrsene na keys v dictionaritata
        if (Input.GetKeyDown(KeyCode.F) && cc != null && canPress)
        {
            cc.gameObject.SetActive(true);
            if (timer - averageFps >= 0)
            {
                cc.timer = timer - averageFps;
            }
            else
            {
                cc.timer = 0;
            }
            cc.counter = 0;
            cc = null;
            canPress = false;
            canRecord = false;
        }
    }
}
