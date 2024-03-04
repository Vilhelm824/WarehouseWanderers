using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrateTimer : MonoBehaviour
{
    public GameObject gameHandler;
    private SpriteRenderer crateRenderer;
    private ParticleSystem explosion;
    private Collider2D crateCollider;
    private AudioSource explosionAudio;
    private Color crateColor = new Color(1f, 1f, 1f, 1f);   
    public float destroyRate = 1f;
    private float currentAlpha = 1f;
    private bool hasExploded = false;
    

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        crateRenderer = GetComponentInChildren<SpriteRenderer>();
        crateCollider = GetComponent<Collider2D>();
        explosionAudio = GetComponent<AudioSource>();
        currentAlpha = 1f;
        crateRenderer.color = crateColor;
        hasExploded = false;
        crateCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAlpha();

        if (currentAlpha <= 0.1f && !hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // disable collider, make invisible, activate particle FX
        crateCollider.enabled = false;
        currentAlpha = 0;
        explosionAudio.Play();
        explosion.Play();
        hasExploded = true;
        // update gamehandler explosion count
        gameHandler.GetComponent<GameHandler>().PackageExploded(gameObject.transform.position);
        StartCoroutine(DestroyCrate(gameObject));
    }

    private void ChangeAlpha()
    {
        // increment the alpha value towards 0
        currentAlpha = Mathf.MoveTowards(currentAlpha, 0, destroyRate * Time.deltaTime);
        crateColor.a = currentAlpha;
        crateRenderer.color = crateColor;
    }

    IEnumerator DestroyCrate(GameObject theCrate)
    {
        yield return new WaitForSeconds(5f);
        Destroy(theCrate);
    }
}
