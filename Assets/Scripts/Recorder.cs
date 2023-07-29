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

    // Start is called before the first frame update
    void Start()
    {
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (timer - 400 >= 0)
            {
                cc.timer = timer - 400;
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
