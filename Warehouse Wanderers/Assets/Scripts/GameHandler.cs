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
    public TileBase fireTile;
    public float minXSpawn = -5f; // Minimum x-coordinate for package spawn
    public float maxXSpawn = 5f; // Maximum x-coordinate for package spawn
    public float minYSpawn = -3f; // Minimum y-coordinate for package spawn
    public float maxYSpawn = 3f; // Maximum y-coordinate for package spawn
    public int maxExplosions = 3;
    private float elapsedTime = 0f; // Time elapsed since last package spawn
    private float nextSpawnTime; // Time when the next package should spawn
    public static int numDelivered;
    private int consecExploded = 0;
    private Vector3Int packagePosInt;
    private static bool spawningActive = true;

    // UI Components
    public Text scoreText;
    public GameObject pauseMenu;

    private void Start()
    {
        //PlayerHandler playerHandler = new PlayerHandler();
        this.GetComponent<PlayerHandler>().SetGameHandler(this);
        CalculateNextSpawnTime();
        consecExploded = 0;
        pauseMenu.SetActive(false);
        numDelivered = 0;
        spawningActive = true;
    }

    private void Update()
    {
        if (!spawningActive) {
            return;
        }

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
        spawningActive = false;
        SceneManager.LoadScene("EndScene");
    }

    private void UpdateScoreText() {
        scoreText.text = "" + numDelivered;
    }
    public void OnPauseButtonPressed() {
        Debug.Log("pause button pressed");
        pauseMenu.SetActive(true);
        spawningActive = false;
    }

    public void OnCloseButtonPressed() {
        Debug.Log("close button pressed");
        pauseMenu.SetActive(false);
        spawningActive = true;
    }

    IEnumerator SpawnFire(Vector3Int packagePosInt)
    {
        yield return new WaitForSeconds(0.5f);
        fireTM.SetTile(packagePosInt, fireTile);
    }
}