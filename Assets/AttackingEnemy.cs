using UnityEngine;

public class AttackingEnemy : Enemy
{
    public State state;

    public float chasingSpeed;

    Transform target;

    private void Update()
    {
        DetectPlayer();
        switch (state)
        {
            case State.PATROL:
                Moving();
                break;

            case State.CHASE:
                Chase();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            Flip();
        }
    }

    public void DetectPlayer()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, LayerMask.GetMask("Player"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, LayerMask.GetMask("Player"));

        Transform playerTransform = null;

        if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
        {
            playerTransform = hitLeft.collider.transform;
        }

        if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
        {
            if (playerTransform == null || Vector2.SignedAngle(Vector2.left, hitRight.collider.transform.position - transform.position) < Vector2.SignedAngle(Vector2.left, playerTransform.position - transform.position))
            {
                playerTransform = hitRight.collider.transform;
            }
        }

        target = playerTransform;
        state = target != null ? State.CHASE : State.PATROL;
    }

    private void Chase() {
        Vector2 dir = (target.transform.position - transform.position).normalized;

        transform.Translate(new Vector2(dir.x * chasingSpeed * Time.deltaTime,0));
    }
}

public enum State
{
    PATROL,
    CHASE
}
