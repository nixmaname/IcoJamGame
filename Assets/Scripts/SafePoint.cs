using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    public int index;
    public Sprite openFlag;

  
    public void RaiseFlag()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = openFlag;
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
