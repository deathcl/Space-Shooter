using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    GameController gameController;
    public int scoreValue;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Asteroid") || other.CompareTag("Enemy")) return;
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
