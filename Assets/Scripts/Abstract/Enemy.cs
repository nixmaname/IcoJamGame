using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int speed;

    public bool vertical;

    private bool isFacingRight;

    public virtual void Moving()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            transform.eulerAngles = !vertical ? new Vector3(0f, -180f, 0f) : new Vector3(-180f, 0f, 90f);
        }
        else
        {
            transform.eulerAngles = !vertical ? Vector3.zero : new Vector3(0f, 0f, 90f);
        }
    }


}
