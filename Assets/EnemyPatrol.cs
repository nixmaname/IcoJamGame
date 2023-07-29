using UnityEngine;

public class EnemyPatrol : Enemy
{

    private void Update()
    {
        Moving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            Flip();
        }
    }
}
