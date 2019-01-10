using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    AudioSource audioSource;
    public float delay; //espera inicial para disparar
    public float fireRate; //cada cuanto dispara

    void Start()
    {
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
