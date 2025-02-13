using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBall : MonoBehaviour
{
    private string targetTag;
    public ParticleSystem RedBox, GreenBox, BlueBox, YellowBox, BrownBox;
    public ParticleSystem hit;

    public void SetTargetTag(string tags)
    {
        targetTag = tags;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            //Handheld.Vibrate();
            ParticleSystem particleSystem = GetParticleSystemByTag(targetTag);
            if (particleSystem != null)
            {
                ParticleSystem instantiatedParticles = Instantiate(particleSystem, transform.position, Quaternion.identity);
                ParticleSystem hitParticles = Instantiate(hit, transform.position, Quaternion.identity);
                instantiatedParticles.Play();
                hitParticles.Play();

                Destroy(instantiatedParticles.gameObject, instantiatedParticles.main.duration);
                Destroy(hitParticles.gameObject, hit.main.duration);
            }

            gameObject.SetActive(false);
        }
    }
    private ParticleSystem GetParticleSystemByTag(string tag)
    {
        switch (tag)
        {
            case "RedBox":
                return RedBox;
            case "GreenBox":
                return GreenBox;
            case "BlueBox":
                return BlueBox;
            case "YellowBox":
                return YellowBox;
            case "BrownBox":
                return BrownBox;
            default:
                return null;
        }
    }
}
