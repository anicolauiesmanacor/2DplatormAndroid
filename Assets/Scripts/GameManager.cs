using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject targets;
    [SerializeField] private GameObject touchpad;
    [SerializeField] private GameObject fairy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject welcome;
    public bool startGame = false;
    public bool isGameOver = false;
    public bool isGreen = false;
    public bool isBlue = false;
    public bool isFlower = false;
    public bool isCloud = false;
    private bool groundTransition = false;
    [SerializeField] private float fadeSpeed;
    private bool skyTransition = false;
    [SerializeField] private GameObject groundGrey;
    [SerializeField] private GameObject groundGreen;
    [SerializeField] private GameObject skyGrey;
    [SerializeField] private GameObject skyBlue;
    private bool cloudsTransition = false;
    [SerializeField] private GameObject clouds;
    private bool natureTransition = false;
    [SerializeField] private GameObject nature;

    void Start () {
        ResetGame();
        startGame = false;
    }

    void Update() {
        if (groundTransition) {
            if (!isGreen) {
                FadeOutRecursive(groundGrey.transform);
                groundGreen.SetActive(true);
                FadeInRecursive(groundGreen.transform);
                isGreen = true;
                groundTransition = false;
            }
        } else if (skyTransition) {
            if (!isBlue) {
                FadeOutRecursive(skyGrey.transform);
                skyBlue.SetActive(true);
                skyBlue.transform.GetChild(0).transform.position = skyGrey.transform.GetChild(0).transform.position;
                FadeInRecursive(skyBlue.transform);
                isBlue = true;
                skyTransition = false;
            }
        } else if (cloudsTransition) {
            if (!isCloud) {
                FadeOutRecursive(clouds.transform);
                isCloud = true;
                cloudsTransition = false;
            }
        } else if (natureTransition) {
            if (!isFlower) {
                nature.SetActive(true);
                FadeInRecursive(nature.transform);
                isFlower = true;
                natureTransition = false;
            }
        }
         
        if (isGreen && isBlue && isFlower && isCloud) {
            fairy.SetActive(true);
        }

        if (isGameOver) {
            Invoke("ResetGame", 5f);
        }
    }

    private void FadeOutRecursive(Transform parent) {
        foreach (Transform child in parent) {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                StartCoroutine(FadeOut(spriteRenderer));

            // Recursively fade out child's children
            FadeOutRecursive(child);
        }
    }

    private System.Collections.IEnumerator FadeOut(SpriteRenderer spriteRenderer) {
        Color originalColor = spriteRenderer.color;
        float t = 0f;

        while (t < 1f) {
            t += Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0f, t));
            yield return null;
        }
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private void FadeInRecursive(Transform parent) {
        foreach (Transform child in parent) {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) {
                StartCoroutine(FadeIn(spriteRenderer));
            }
            // Recursively fade in child's children
            FadeInRecursive(child);
        }
    }

    private System.Collections.IEnumerator FadeIn(SpriteRenderer spriteRenderer) {
        Color originalColor = spriteRenderer.color;
        originalColor.a = 1f;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0f, originalColor.a, t));
            yield return null;
        }
        spriteRenderer.color = originalColor;
    }

    public void MakeGroundTransition() {
        groundTransition = true;
    }

    public void MakeBlueSky() {
        skyTransition = true;
    }

    public void GoAwayClouds() {
        cloudsTransition = true;
    }

    public void GrowNature() {
        natureTransition = true;
    }

    public void ResetGame() {
        startGame = isGreen = isBlue = isCloud = isFlower = false;
        groundTransition = skyTransition = cloudsTransition = natureTransition = false;

        FadeOutRecursive(groundGreen.transform);
        groundGreen.SetActive(false);
        groundGrey.SetActive(true);
        FadeInRecursive(groundGrey.transform);

        FadeOutRecursive(skyBlue.transform);
        skyBlue.SetActive(false);
        skyGrey.SetActive(true);
        FadeInRecursive(skyGrey.transform);

        clouds.SetActive(true);
        FadeInRecursive(clouds.transform);
        
        FadeOutRecursive(nature.transform);  
        nature.SetActive(false);

        fairy.SetActive(false);

        player.SetActive(false);
        targets.SetActive(false);
        touchpad.SetActive(false);

        welcome.SetActive(true);
    }
}
