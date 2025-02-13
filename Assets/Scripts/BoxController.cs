using UnityEngine;

public class BoxController : MonoBehaviour
{
    private float rayDistance=.8f;
    public LayerMask cubeLayer;
    private float lerpSpeed = 25f;

    public Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Perform a raycast to check for the box in front
        Ray ray = new Ray(transform.position, Vector3.back);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, cubeLayer))
        {
            targetPosition = transform.position;
        }
        else
        {
            targetPosition = transform.position + Vector3.back;
        }

        if (targetPosition != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }
    }
}
