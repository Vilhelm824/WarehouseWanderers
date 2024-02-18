using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateTimer : MonoBehaviour
{
    private SpriteRenderer crate;
    private Color crateColor = new Color(255, 255, 255, 255);
    private float colorChange = 255f;
    public float timeToDestroy = 5f;

    // Start is called before the first frame update
    void Start()
    {
        crate = GetComponentInChildren<SpriteRenderer>();
        crate.color = crateColor;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if(timeToDestroy <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
