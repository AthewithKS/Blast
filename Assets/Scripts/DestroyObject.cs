using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //public BallObjectPooling pooling;

    public float ballSpeed;

    public float rayDistance = 100f;
    public LayerMask cubeLayer;
    public Transform shootPoint;

    public SelectTag selectedTag;
    public Transform LookPointParent;
    
    private GameObject lastHitObject = null;
    private float lastShotTime = 0f;
    public float shootCoolDown;

    private bool isReadyToShoot = false;
    public enum SelectTag 
    {
        RedBox,GreenBox,BlueBox,YellowBox,BrownBox
    }


    private void FixedUpdate()
    {
        if (!isReadyToShoot) return;
        if(Time.time-lastShotTime>=shootCoolDown) 
        {
           RayToFire();
            lastShotTime = Time.time;
        }
    }
    public void RayToFire()
    {

        foreach (Transform LookPoint in LookPointParent)
        {
            shootPoint.LookAt(LookPoint);
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, cubeLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject != lastHitObject && hit.collider.CompareTag(selectedTag.ToString()))
                {
                    this.transform.LookAt(hit.point);

                    GameObject ball = BallObjectPooling.SharedInstance.GetFromPool();
                    ball.transform.position = shootPoint.position;
                    ball.transform.rotation = Quaternion.identity;
                    ball.SetActive(true);

                    DeactivateBall ballScript = ball.GetComponent<DeactivateBall>();
                    if (ballScript != null)
                    {
                        ballScript.SetTargetTag(selectedTag.ToString());
                    }

                    Vector3 direction = hit.point - shootPoint.position;
                    Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                    if (ballRigidbody != null)
                    {
                        ballRigidbody.velocity = direction * ballSpeed;
                    }
                    lastHitObject = hitObject;
                    break;
                }
                Debug.DrawLine(shootPoint.position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(shootPoint.position, shootPoint.position + shootPoint.forward * rayDistance, Color.red);
            }
        }
    }

    private void OnMouseDown()
    {
        isReadyToShoot = true;
    }
}
