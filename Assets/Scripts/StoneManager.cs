using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour {
    [SerializeField] private int type;
    
    [SerializeField] private float amplitude = 0.5f;  // Controls the height of the float
    [SerializeField] private float speed = 1f;       // Controls the speed of the float

    private GameManager gmanager;

    private Vector2 originalPosition;
    private float time;
    
    void Start() {
        originalPosition = transform.position;
        gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        time += Time.deltaTime * speed;
        float yOffset = Mathf.Sin(time) * amplitude;
        transform.position = originalPosition + new Vector2(0f, yOffset);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            switch (type) {
                case 0:
                    if (!gmanager.isGreen) {
                        gmanager.MakeGroundTransition();
                        Destroy(this.gameObject);
                    }
                    break;

                case 1:
                    if (!gmanager.isBlue) {
                        gmanager.MakeBlueSky();
                        Destroy(this.gameObject);
                    }
                    break;

                case 2:
                    if (!gmanager.isCloud) {
                        gmanager.GoAwayClouds();
                        Destroy(this.gameObject);
                    }
                    break;

                case 3:
                    if (!gmanager.isFlower) {
                        gmanager.GrowNature();
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
    }
}
