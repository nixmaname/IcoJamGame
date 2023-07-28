using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int speed;

    public GameObject waypointA, waypointB;

    public abstract void Moving();

}
