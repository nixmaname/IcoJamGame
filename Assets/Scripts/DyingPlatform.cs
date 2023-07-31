using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingPlatform : MonoBehaviour
{
    Animator anim;
    AudioSource aud;
    private void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    public void AnimStart()
    {
        aud.Play();
        anim.Play("DyingPlatformBig",0,0f);
    }
    public void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
