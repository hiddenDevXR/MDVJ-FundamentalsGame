using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    public int direction = 1;
    public ParticleSystem shotVFX;
    [SerializeField]
    private AudioSource shotSound;

    public void Shoot()
    {
        GameObject instance = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        instance.GetComponent<Projectile>().direction = direction;
        shotVFX.Play();
        shotSound.Play();
    }
}
