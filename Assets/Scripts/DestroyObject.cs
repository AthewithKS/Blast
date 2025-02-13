using System.Collections;
using TMPro;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    private float ballSpeed =5f;
    public int BallCount;
    public TMP_Text ballCountText;
    public Transform moveOutTransform;

    private float rayDistance = 100f;
    public LayerMask cubeLayer;
    public Transform shootPoint;

    public SelectTag selectedTag;
    public Transform LookPointParent;
    
    private GameObject lastHitObject = null;
    private float lastShotTime = 0f;
    private float shootCoolDown=0.25f;

    public float moveSpeed = 10f;
    private int currentPlatformIndex = -1;

    private bool isReadyToShoot = false;

    public enum SelectTag 
    {
        RedBox,GreenBox,BlueBox,YellowBox,BrownBox
    }

    private void Start()
    {
        ballCountText.text = BallCount.ToString();
    }
    private void FixedUpdate()
    {
        if (!isReadyToShoot || BallCount<=0) return;
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

                    UpdateBallCount();

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
            }
        }
    }
    public void UpdateBallCount()
    {
        BallCount--;
        ballCountText.text = BallCount.ToString();
        if(BallCount <= 0)
        {
            Invoke("MoveOut", 0.5f);
        }
    }
    public void MoveOut()
    {
        transform.LookAt(moveOutTransform);
        PlatFormManager.instance.FreePlatform(currentPlatformIndex);
        StartCoroutine(MoveToPlatform(moveOutTransform));
        Invoke("DestroyGameObj", 2f);
    }
    private void DestroyGameObj()
    {
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        CheckAndMoveToPlatform();
    }
    private void CheckAndMoveToPlatform()
    {
        currentPlatformIndex = PlatFormManager.instance.GetAvailablePlatform();
        if (currentPlatformIndex != -1)
        {
            Transform platform = PlatFormManager.instance.GetPlatformTransform(currentPlatformIndex);
            transform.LookAt(platform);
            StartCoroutine(MoveToPlatform(platform));
        }
    }
    private IEnumerator MoveToPlatform(Transform platform)
    {
        while (Vector3.Distance(transform.position, platform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, platform.position, moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        transform.position = platform.position;

        isReadyToShoot = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(currentPlatformIndex != -1 && other.transform==PlatFormManager.instance.GetPlatformTransform(currentPlatformIndex))
        {
            PlatFormManager.instance.FreePlatform(currentPlatformIndex);
        }
    }
}
