using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
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
                FadeOutRecursive(nature.transform);
                isFlower = true;
                natureTransition = false;
            }
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
}