using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Game components
    public float minSpawnTime = 10f; // Minimum time between package spawns
    public float maxSpawnTime = 30f; // Maximum time between package spawns
    public GameObject crateTemplate; // crate object to clone & spawn (should be disabled in scene)
    public float minXSpawn = -8f; // Minimum x-coordinate for package spawn
    public float maxXSpawn = 8f; // Maximum x-coordinate for package spawn
    public float minYSpawn = -4.5f; // Minimum y-coordinate for package spawn
    public float maxYSpawn = 4.5f; // Maximum y-coordinate for package spawn
    public int maxExplosions = 3;
    private float elapsedTime = 0f; // Time elapsed since last package spawn
    private float nextSpawnTime; // Time when the next package should spawn
    private static int numDelivered;
    private int consecExploded = 0;

    // UI Components
    public Text scoreText;
    public Canvas pauseMenu;

    private void Start()
    {
        // PlayerHandler playerHandler = new PlayerHandler();
        // playerHandler.SetGameHandler(this);
        CalculateNextSpawnTime();
        consecExploded = 0;
    }

    private void Update()
    {
        if(consecExploded >= maxExplosions)
        {
            GameEnd();
        }
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= nextSpawnTime)
        {
            SpawnPackage();
            CalculateNextSpawnTime();
        }

        UpdateScoreText();
    }

    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = Random.Range(elapsedTime + minSpawnTime, elapsedTime + maxSpawnTime);
    }

    private void SpawnPackage()
    {
        float xPos = Random.Range(minXSpawn, maxXSpawn);
        float yPos = Random.Range(minYSpawn, maxYSpawn);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);
        Instantiate(crateTemplate, spawnPos, Quaternion.identity).SetActive(true);
    }

    public void PackageExploded()
    {
        consecExploded++;
        Debug.Log("num exploded: " + consecExploded);
    }

    public void PackageDelivered()
    {
        consecExploded = 0;
        numDelivered++;
    }

    private void GameEnd()
    {
        // End state
        SceneManager.LoadScene("EndScene");
    }

    public void Replay() {
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateScoreText() {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GameScene") {
            scoreText.text = "" + numDelivered;
            scoreText.gameObject.SetActive(true);
        }
        else if (currentScene.name == "EndScene") {
            scoreText.text = "You picked up " + numDelivered + " packages!";
            scoreText.gameObject.SetActive(true);
        }
    }
    public void OnPauseButtonPressed() {
        pauseMenu.gameObject.SetActive(true);
    }

    public void OnCloseButtonPressed() {
        pauseMenu.gameObject.SetActive(false);
    }
}