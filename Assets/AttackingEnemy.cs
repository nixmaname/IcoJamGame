using UnityEngine;

public class AttackingEnemy : Enemy
{
    public State state;

    public float chasingSpeed;

    public Transform waypointA, waypointB;

    private Transform target;

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
        float distToA = Vector2.Distance(waypointA.position, transform.position);
        float distToB = Vector2.Distance(waypointB.position, transform.position);

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, waypointA.position - transform.position, distToA, LayerMask.GetMask("Player"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, waypointB.position - transform.position, distToB, LayerMask.GetMask("Player"));

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

    private void Chase()
    {
        if (target.position.x > waypointA.position.x && target.position.x < waypointB.position.x)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;

            float dotProduct = Vector2.Dot(transform.right, dir.normalized);

            if (dotProduct < 0 && isFacingRight)
                Flip();
            else if (dotProduct > 0 && !isFacingRight)
                Flip();

            transform.Translate(new Vector2(dir.x * chasingSpeed * Time.deltaTime, 0));
        }
        else
        {
            target = null;
            state = State.PATROL;
        }
    }
}

public enum State
{
    PATROL,
    CHASE
}
