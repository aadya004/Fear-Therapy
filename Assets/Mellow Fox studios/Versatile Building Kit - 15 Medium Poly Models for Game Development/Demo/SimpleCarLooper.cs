using UnityEngine;

public class SimpleCarLooper : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;

    private Vector3 direction;

    void Start()
    {
        transform.position = startPoint.position;
        direction = (endPoint.position - startPoint.position).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        Vector3 toEnd = endPoint.position - transform.position;
        if (Vector3.Dot(toEnd, direction) < 0)
        {
            transform.position = startPoint.position;
        }
    }
}
