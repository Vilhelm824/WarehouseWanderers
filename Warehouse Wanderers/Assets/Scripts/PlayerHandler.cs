using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    bool isHolding = false;
    GameObject package;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropOff()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHolding = false;
        }
    }

    void Throw()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // picks up the crate
        package = other.gameObject;
        other.gameObject.SetActive(false);
        isHolding = true;
    }
}
