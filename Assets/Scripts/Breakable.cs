using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject Cube;
    [SerializeField] GameObject BrokenCube;
    List<Rigidbody> BrokenCubeList;
    BoxCollider bc;

    private void Awake()
    {
        Cube.SetActive(true);
        BrokenCube.SetActive(false);
        
        bc=GetComponent<BoxCollider>();
    }
    private void BreakObject()
    {
        Cube.SetActive(false);
        BrokenCube.SetActive(true);

        bc.enabled = false;

        BrokenCubeList = new List<Rigidbody>(BrokenCube.GetComponentsInChildren<Rigidbody>());

        // Apply random force to each rigidbody
        foreach (Rigidbody rb in BrokenCubeList)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddForce(randomDirection.normalized * Random.Range(5f, 10f), ForceMode.Impulse); // Adjust force as needed
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BreakObject();

            DeactivateIn();
        }
    }
    IEnumerator DeactivateIn()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
