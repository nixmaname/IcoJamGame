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
    CameraShake camShake;
    PlayerSafe playerSafe;

    int fps;
    float sec;
    int averageFps;
    GameObject[] platforms;
    GameObject[] phantomPlatform;
    GameObject[] phantomCoin;

    public AudioSource aud;
    public AudioClip countSFX;


    // Start is called before the first frame update
    void Start()
    {
        oldpos = transform.position;
        camShake = Camera.main.transform.GetComponent<CameraShake>();
        playerSafe = GetComponent<PlayerSafe>();
        platforms = GameObject.FindGameObjectsWithTag("TempPlatform");
        phantomPlatform = GameObject.FindGameObjectsWithTag("PhantomP");
        phantomCoin = GameObject.FindGameObjectsWithTag("Coin");
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (sec < 1)
        {
            sec += Time.deltaTime;
            fps++;
        }
        else
        {
            sec = 0;
            averageFps = fps * 4;
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
            aud.clip = countSFX;
            aud.Play();
            camShake.StartCoroutine(camShake.Shake());

            canPress = false;
        }

    }
    public void Unalive()
    {
        transform.GetComponent<PlayerController>().imobilize = true;
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
        transform.position = playerSafe.lastSafepoint;
        cc.transform.position = transform.position;
        cc = null;
        canRecord = false;
        foreach (var dp in platforms)
        {
            if (!dp.activeSelf)
                dp.SetActive(true);
        }
        foreach (var phantom in phantomPlatform)
        {
            phantom.GetComponent<BoxCollider2D>().enabled = false;
            phantom.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.25f);
        }
        foreach (var coin in phantomCoin)
        {
            coin.SetActive(true);
        }
    }

    public void Restart()
    {
        foreach (var dp in platforms)
        {
            if (!dp.activeSelf)
                dp.SetActive(true);
        }
        foreach (var phantom in phantomPlatform)
        {
            phantom.GetComponent<BoxCollider2D>().enabled = false;
            phantom.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
        }
        foreach (var coin in phantomCoin)
        {
            coin.SetActive(true);
        }
        if (cc == null)
            cc = GameObject.FindGameObjectWithTag("Ghost").GetComponent<CopyCat>();
        transform.GetComponent<PlayerController>().imobilize = true;
        cc.counter = 0;
        transform.position = playerSafe.lastSafepoint;
        cc.transform.position = transform.position;
        oldpos = transform.position;
        timer = 0;
        cc.AngelReset();
        camShake.timeLeft = 0;
        canPress = true;
        canRecord = true;


    }


}
