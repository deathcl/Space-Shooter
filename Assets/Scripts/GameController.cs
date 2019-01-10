using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    int puntos;
    public Text textPuntos;
    public GameObject restartGameObject;
    public GameObject gameOverGameObject;
    bool restart;
    bool gameOver;

    void Start()
    {
        restart = false;
        gameOver = false;
        gameOverGameObject.SetActive(false);
        restartGameObject.SetActive(false);
        puntos = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        UpdateBoundary();
    }

    void UpdateBoundary()
    {
        //calculamos la mitad de la pantalla en alto y ancho
        Vector2 half = Utils.GetDimensionsInWorldUnit() / 2;
        //asignamos los valores para que los asteroides salgan solo en el area de la pantalla que se ve y no fuera de ella
        spawnValues = new Vector3(half.x - 0.7f, 0f, half.y + 7f);
    }

    void Update()
    {
        if(restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                //elejimos de forma aleatoria entre todos los prefabs de asteroides que tenemos dentro del array hazards
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait); //al llegar aqui se pospondra el siguiente ciclo hasta que pase el tiempo definido
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartGameObject.SetActive(true);
                restart = true;
                break;
            }
        }
        
    }

    public void AddScore(int value)
    {
        puntos += value;
        UpdateScore();
    }

    void UpdateScore()
    {
        textPuntos.text = "Puntos: " + puntos;
    }

    public void GameOver()
    {
        gameOverGameObject.SetActive(true);
        gameOver = true;
    }
}
