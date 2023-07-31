using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    public Vector2 parralaxSmoothing;

    private Transform cam;
    private Vector3 oldPos;

    private void Start()
    {
        cam = Camera.main.transform;
        oldPos = cam.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cam.position - oldPos;

        transform.position += new Vector3(delta.x * parralaxSmoothing.x, delta.y * parralaxSmoothing.y, 0);
        oldPos = cam.position;
    }
}
