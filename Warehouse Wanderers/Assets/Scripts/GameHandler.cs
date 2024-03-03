using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Game components
    public float minSpawnTime = 5f; // Minimum time between package spawns
    public float maxSpawnTime = 10f; // Maximum time between package spawns
    public GameObject crateTemplate; // crate object to clone & spawn (should be disabled in scene)
    public Tilemap fireTM;
    public Tile fireTile;
    public float minXSpawn = -5f; // Minimum x-coordinate for package spawn
    public float maxXSpawn = 5f; // Maximum x-coordinate for package spawn
    public float minYSpawn = -3f; // Minimum y-coordinate for package spawn
    public float maxYSpawn = 3f; // Maximum y-coordinate for package spawn
    public int maxExplosions = 3;
    private float elapsedTime = 0f; // Time elapsed since last package spawn
    private float nextSpawnTime; // Time when the next package should spawn
    private static int numDelivered;
    private int consecExploded = 0;
    private Vector3Int packagePosInt;

    // UI Components
    public Text scoreText;
    public Canvas pauseMenu;

    private void Start()
    {
        PlayerHandler playerHandler = new PlayerHandler();
        playerHandler.SetGameHandler(this);
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

    public void PackageExploded(Vector3 packagePos)
    {
        consecExploded++;
        Debug.Log("num exploded: " + consecExploded);
        packagePosInt = new Vector3Int(Mathf.FloorToInt(packagePos.x), Mathf.FloorToInt(packagePos.y));
        StartCoroutine(SpawnFire(packagePosInt));
    }

    public void PackageDelivered()
    {
        consecExploded = 0;
        numDelivered++;
        Debug.Log("num delivered: " + numDelivered);
    }

    private void GameEnd()
    {
        // End state
        SceneManager.LoadScene("EndScene");
        SceneManager.UnloadSceneAsync("GameScene");
    }

    public void Replay() {
        Debug.Log("replay button");
        SceneManager.LoadScene("GameScene");
        SceneManager.UnloadSceneAsync("EndScene");
    }

    private void UpdateScoreText() {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GameScene") {
            scoreText.text = "" + numDelivered;
        }
        else if (currentScene.name == "EndScene") {
            scoreText.text = "You picked up " + numDelivered + " packages!";
            scoreText.gameObject.SetActive(true);
        }
    }
    public void OnPauseButtonPressed() {
        Debug.Log("pause button pressed");
        pauseMenu.gameObject.SetActive(true);
    }

    public void OnCloseButtonPressed() {
        Debug.Log("close button pressed");
        pauseMenu.gameObject.SetActive(false);
    }

    IEnumerator SpawnFire(Vector3Int packagePosInt)
    {
        yield return new WaitForSeconds(0.5f);
        fireTM.SetTile(packagePosInt, fireTile);
    }
}