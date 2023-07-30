using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomCoin : MonoBehaviour
{

    public GameObject[] phantomPlatform;


    private void OnDisable()
    {
        foreach (var platform in phantomPlatform)
        {
            platform.GetComponent<SpriteRenderer>().color = Color.white;
            platform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
