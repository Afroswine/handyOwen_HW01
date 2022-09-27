using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public abstract class PowerUpBase : MonoBehaviour
{
    [Header("PowerUpBase")]
    [SerializeField] protected float _powerupDuration;
    [SerializeField] ParticleSystem _powerupParticles;
    [SerializeField] AudioClip _powerupSound;
    protected Player _player;

    protected abstract void PowerUp();
    protected abstract void PowerDown();

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject.GetComponent<Player>();

        // disable powerup visuals and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        StartCoroutine(PowerupCR());
    }

    // CR for "Co-Routine"
    private IEnumerator PowerupCR()
    {
        PowerUp();

        float normalizedTime = 0f;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / _powerupDuration;
            yield return null;
        }

        PowerDown();
        //can i tell this to destroy itself with that yield return null???
        Destroy(gameObject);
    }
}
