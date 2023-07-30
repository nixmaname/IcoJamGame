using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingPlatform : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AnimStart()
    {
        anim.Play("DyingPlatformBig",0,0f);
    }
    public void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
