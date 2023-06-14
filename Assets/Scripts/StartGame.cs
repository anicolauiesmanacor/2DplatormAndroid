using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject player;
    public GameObject targets;
    public GameObject touchpad;
    public GameObject welcome;
    public GameManager gmanager;

    void Start() {
        if (gmanager.startGame)
            MakeTheGameBegin();
    }

    public void MakeTheGameBegin() {
        //gmanager.ResetGame();
        player.SetActive(true);
        targets.SetActive(true);

        foreach (Transform child in targets.transform) {
            child.gameObject.SetActive(true);
        }
        touchpad.SetActive(true);
        gmanager.startGame = true;
        welcome.SetActive(false);

        player.GetComponent<PlayerController>().PlayerDeath();
    }
}
