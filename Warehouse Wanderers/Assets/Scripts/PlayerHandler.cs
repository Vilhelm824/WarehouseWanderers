using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    bool isHolding = false;
    GameObject package;
    public AudioSource pickupSound;
    public AudioSource throwSound;
    public AudioSource dropoffSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isHolding) {
        //package.transform.position = new Vector3(this.transform.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
        //package.transform.rotation = this.transform.rotation;
        //}
        if(Input.GetKey("q") && isHolding)
        {
            Throw();
        }
    }

    void DropOff(Collider2D dropOffLocation)
    {
        dropoffSound.Play();
        package.transform.position = dropOffLocation.transform.position;
        package.transform.rotation = dropOffLocation.transform.rotation;
        isHolding = false;
    }

    void Throw()
    {
        throwSound.Play();
        package.transform.position = gameObject.transform.position;
        package.transform.rotation = gameObject.transform.rotation;
        package.SetActive(true);
        isHolding = false;
        Debug.Log("Threw Crate");
    }

    void Pickup(Collider2D crate)
    {
            // picks up the crate
            package = crate.gameObject;
            package.SetActive(false);
            pickupSound.Play();
            isHolding = true;
            Debug.Log("Picked up crate");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // confirm player is touching a crate, and check if they press space
        if (other.CompareTag("Crate") && Input.GetKey("e") && !isHolding)
        {
            Pickup(other);
        }

        else if (other.CompareTag("DropOff") && isHolding)
        {
            DropOff(other);
        }
    }
}
