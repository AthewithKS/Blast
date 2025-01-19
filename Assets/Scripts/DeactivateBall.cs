using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBall : MonoBehaviour
{
    private string targetTag;

    public void SetTargetTag(string tags)
    {
        targetTag = tags;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
