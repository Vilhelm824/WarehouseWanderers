using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateTimer : MonoBehaviour
{
    SpriteRenderer crate;
    Color crateColor = new Color(255, 255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {
        crate = GetComponentInChildren<SpriteRenderer>();
        crate.color = crateColor;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
