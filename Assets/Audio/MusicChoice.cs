using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChoice : MonoBehaviour
{
    public AudioClip hotAndCold, random;
    AudioSource aud;
    int nl;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        nl = GameObject.FindGameObjectWithTag("EndLevel").transform.GetComponent<NextLevel>().nextLevel-1;
        if(nl%2 == 0)
        {
            aud.clip = hotAndCold;
        }
        else
        {
            aud.clip = random;
        }
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
