using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable] //hacemos la clase publica dentro del inspector de unity
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 10f;
    public float tilt = 4f; //inclinacion de la nave
    Rigidbody rb;
    public Boundary boundary;

    [Header("Disparo")]
    public GameObject shot; //referencia para el prefab del disparo
    public Transform shotSpawn; //referencia para el objeto que contiene la posicion del disparo
    public float fireRate = 0.15f; //tasa de disparo, cuantos disparos por segundo podemos hacer
    private float nextFire; //cuantos segundos podemos volver a disparar
    AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateBoundary();
    }

    void UpdateBoundary()
    {
        //calculamos la mitad de la pantalla en alto y ancho
        Vector2 half = Utils.GetDimensionsInWorldUnit() / 2;
        boundary.xMin = -half.x + 0.7f;
        boundary.xMax = half.x - 0.7f;
        boundary.zMin = -half.y + 8f;
        boundary.zMax = half.y - 2f;
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            audioSource.Play();
        }       
    }

    void FixedUpdate()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * speed;
        //codigo para que la nave haga una rotacion en el eje X y Z para el efecto de rotacion
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),0, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        //velocidad con la que rotara la nave hacia los lados
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }
}
