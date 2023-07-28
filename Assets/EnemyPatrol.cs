using UnityEngine;

public class EnemyPatrol : Enemy
{
    private bool isFacingRight;

    private void Update()
    {
        Moving();
    }

    public override void Moving()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            transform.eulerAngles = new Vector2(0, -180);
        }
        else
        {
            transform.eulerAngles = Vector2.zero;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            Flip();
        }
    }
}
