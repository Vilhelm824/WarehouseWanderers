using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    bool isHolding = false;
    GameObject package;
    private static GameHandler gameHandlerInstance;
    public AudioSource pickupSound;
    public AudioSource throwSound;
    public AudioSource dropoffSound;
    public GameObject playerSprite;
    public GameObject holdingSprite;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("q") && isHolding)
        {
            Throw();
        }
    }

    public void SetGameHandler(GameHandler gameHandler) {
        gameHandlerInstance = gameHandler;
    }

    void DropOff(Collider2D dropOffLocation)
    {
        dropoffSound.Play();
        package.transform.position = dropOffLocation.transform.position;
        package.transform.rotation = dropOffLocation.transform.rotation;
        isHolding = false;
        UpdateSprite();
        gameHandlerInstance.PackageDelivered();
    }

    void Throw()
    {
        throwSound.Play();
        package.transform.position = gameObject.transform.position;
        package.transform.rotation = gameObject.transform.rotation;
        package.SetActive(true);
        isHolding = false;
        UpdateSprite();
        Debug.Log("Threw Crate");
    }

    void Pickup(Collider2D crate)
    {
        // picks up the crate
        package = crate.gameObject;
        package.SetActive(false);
        pickupSound.Play();
        isHolding = true;
        UpdateSprite();
        Debug.Log("Picked up crate");
    }


    void UpdateSprite()
    {
        playerSprite.SetActive(!isHolding);
        holdingSprite.SetActive(isHolding);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("hi");
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
