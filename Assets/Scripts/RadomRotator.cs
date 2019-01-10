using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomRotator : MonoBehaviour
{
    public float tumble;
    Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Vector3 angularVelocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        rig.angularVelocity = Random.insideUnitSphere.normalized * tumble;
        float mesure = Random.Range(0.5f, 3f);
        rig.transform.localScale = new Vector3(mesure, mesure, mesure);
    }
}
