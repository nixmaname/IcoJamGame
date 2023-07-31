using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int speed;

    public bool vertical;

    protected bool isFacingRight;

    public virtual void Moving()
    {
        Vector2 moveDir = !isFacingRight ? Vector2.right : Vector2.left;
        transform.Translate(moveDir * speed * Time.deltaTime, Space.Self);
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;

        //if (isFacingRight)
        //{
        //    transform.eulerAngles = !vertical ? new Vector3(0f, -180f, 0f) : new Vector3(-180f, 0f, 90f);
        //}
        //else
        //{
        //    transform.eulerAngles = !vertical ? Vector3.zero : new Vector3(0f, 0f, 90f);
        //}
    }


}
