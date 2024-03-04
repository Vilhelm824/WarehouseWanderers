using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneHandler : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "You picked up " + GameHandler.numDelivered + " packages!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay() {
        SceneManager.LoadScene("GameScene");
    }
}
