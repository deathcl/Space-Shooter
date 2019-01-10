using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveMovement : MonoBehaviour
{
    public Vector2 startWait; //vector que nos permite guardar las 2 variables min y max para la espera de el movimiento evasivo
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge; //velocidad maxima para la maniobra evasiva
    public float smoothing;
    public float tilt = 4f; //inclinacion de la nave
    float targetManeuver;
    Rigidbody rig;
    public Boundary boundary;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        UpdateBoundary();
        StartCoroutine(Evade());
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

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            //Mathf.Sign nos devuelve 1 si el valor que le damos es 0 o mayor y -1 si es negativo
            //es lo mismo que multiplicar por (transform.position.x < 0 ? 1 : -1)
            targetManeuver = Random.Range(1f, dodge) * -Mathf.Sign(transform.position.x);
            //targetManeuver = Random.Range(1f, dodge) * Random.Range(-3, 3);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rig.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rig.velocity = new Vector3(targetManeuver, 0, rig.velocity.z);
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0, rig.position.z);
        rig.rotation = Quaternion.Euler(0, 0, rig.velocity.x * -tilt);
    }
}
