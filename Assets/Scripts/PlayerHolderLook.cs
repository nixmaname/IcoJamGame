using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolderLook : MonoBehaviour
{
    Vector2 shootPos;
    Quaternion shootRot;
    bool extrudeRope;
    public SpriteRenderer shotRope;

    public LayerMask groundAndWall;
    public float multiplier = 9f;
    Rigidbody2D rb;
    Vector2 dir;
    public PlayerController pc;
    public float ropeForce;
    public float mainRopeForce = 45f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (extrudeRope)
        {

            shotRope.size = new Vector2(1, Vector2.Distance(transform.position, shootPos) * multiplier);
            dir = shootPos - new Vector2(transform.position.x, transform.position.y);

            // Calculate the angle in radians between the object's forward vector and the direction
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Rotate the object towards the mouse position
            shotRope.transform.rotation = Quaternion.AngleAxis(ang - 90, Vector3.forward);

            if (shotRope.size.y < 5)
            {
                shotRope.size = Vector2.one;
                extrudeRope = false;
                rb.isKinematic = false;
                pc.enabled = true;
            }
            else
            {
                pc.transform.Translate(dir.normalized*Time.deltaTime*35);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 0)) - transform.position, Mathf.Infinity, groundAndWall);
            if (hit.collider != null)
            {
                shootPos = hit.point;
            }
            rb.isKinematic = true;
            pc.enabled = false;
            extrudeRope = true;
        }
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f; // Make sure the z-coordinate is zero (2D game)

        // Calculate the direction vector from the object to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in radians between the object's forward vector and the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object towards the mouse position
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
