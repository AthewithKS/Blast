using System;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public bool isGameOver = false;
    public float WaitforSec=10f;
    public LayerMask mask;
    public ParticleSystem victoryParticle;

    public float rayDistance = 100f;

    public GameObject VictoryPane;

    private void Start()
    {
        victoryParticle.Stop();
    }
    private void Update()
    {
        if(WaitforSec>=0)
        {
            WaitforSec -= Time.deltaTime;
        }
        if (!isGameOver && WaitforSec <= 0)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, mask))
            {
                Debug.DrawLine(transform.position, hit.point,Color.green);
            }
            else
            {
                isGameOver = true;
                VictoryPane.SetActive(isGameOver);
                victoryParticle.Play();
            }
        }
    }
}
