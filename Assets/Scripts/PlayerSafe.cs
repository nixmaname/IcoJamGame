using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafe : MonoBehaviour
{
    public Vector2 lastSafepoint;
    public Transform startPos;
    int lastIndex;

    private void Start()
    {
        lastSafepoint = startPos.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafePoint"))
        {
            SafePoint sp = other.transform.GetComponent<SafePoint>();
            int i = sp.index;
            if (i > lastIndex)
            {
                lastIndex = i;
                lastSafepoint = other.transform.position;
                sp.RaiseFlag();
                Destroy(other);
            }
        }
    }
}
