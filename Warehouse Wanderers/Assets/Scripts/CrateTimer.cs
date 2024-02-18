using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrateTimer : MonoBehaviour
{
    private SpriteRenderer crateRenderer;
    private Color crateColor = new Color(255, 255, 255, 255);   
    public float timeToDestroy = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        crateRenderer = GetComponentInChildren<SpriteRenderer>();
        crateRenderer.color = crateColor;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        Debug.Log(crateRenderer.color);

        if(timeToDestroy <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
