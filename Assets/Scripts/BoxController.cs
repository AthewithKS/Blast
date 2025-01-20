using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float rayDistance=.7f;
    public LayerMask cubeLayer;
    public float lerpSpeed = 13f;

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
            SetTargetPosition(hit.transform.position);
        }
        else
        {
            if (targetPosition != transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            }
        }
    }

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }
}
