using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    bool isHolding = false;
    GameObject package;
    float shiftAmount = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding) {
        package.transform.position = new Vector3(this.transform.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
        package.transform.rotation = this.transform.rotation;
        }
    }

    void DropOff(Collider2D dropOffLocation)
    {
        package.transform.position = dropOffLocation.transform.position;
        package.transform.rotation = dropOffLocation.transform.rotation;
        isHolding = false;
    }

    void Throw()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crate"))
        {
            if (!isHolding) {
            // picks up the crate
            package = other.gameObject;
            isHolding = true;
            }
        }

        else if (other.CompareTag("DropOff"))
        {
            DropOff(other);
        }
    }
}
