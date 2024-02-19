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
    private Color crateColor = new Color(1f, 1f, 1f, 1f);   
    public float destroyRate = 1f;
    private float desiredAlpha = 0f;
    private float currentAlpha = 1f;
    private bool hasExploded = false;
    

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        crateRenderer = GetComponentInChildren<SpriteRenderer>();
        crateCollider = GetComponent<Collider2D>();
        crateRenderer.color = crateColor;
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, destroyRate * Time.deltaTime);
        crateColor.a = currentAlpha;
        crateRenderer.color = crateColor;

        if(currentAlpha <= 0.1f && !hasExploded)
        {
            crateCollider.enabled = false;
            currentAlpha = 0;
            explosion.Play();
            hasExploded = true;
            gameHandler.GetComponent<GameHandler>().PackageExploded();
            StartCoroutine(DestroyCrate(gameObject));
            // gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyCrate(GameObject theCrate)
    {
        yield return new WaitForSeconds(15f);
        Destroy(theCrate);
    }
}
