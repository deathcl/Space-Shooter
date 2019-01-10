using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody rig;
    public float speed = -5f;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rig.velocity = transform.forward * Random.Range(speed, speed * 2.5f);
    }
}
