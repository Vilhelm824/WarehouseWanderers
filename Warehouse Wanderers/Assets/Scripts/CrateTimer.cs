using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrateTimer : MonoBehaviour
{
    private SpriteRenderer crateRenderer;
    private Color crateColor = new Color(1f, 1f, 1f, 1f);   
    public float destroyRate = 1f;
    private float desiredAlpha = 0f;
    private float currentAlpha = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        crateRenderer = GetComponentInChildren<SpriteRenderer>();
        crateRenderer.color = crateColor;
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, destroyRate * Time.deltaTime);
        crateColor.a = currentAlpha;
        crateRenderer.color = crateColor;

        Debug.Log(crateRenderer.color);

        if(currentAlpha <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
