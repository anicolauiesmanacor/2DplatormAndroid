using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField] private Transform[] waypoints;  // An array of waypoints the platform will move between
    public float speed = 3f;      // The speed at which the platform moves
    private int currentWaypoint = 0;  // The index of the current waypoint
    private bool movingPlatform = false;
    private bool isOnPlatform = false;
    private GameObject player;
    private Vector3 platformOffset;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate() {
        // Check if the platform has reached the current waypoint
        if (movingPlatform) {
            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f) {
                // If the platform reached the last waypoint, reset to the first waypoint
                if (currentWaypoint == waypoints.Length - 1)
                    currentWaypoint = 0;
                else
                    currentWaypoint++;
            }

            // Move the platform towards the current waypoint
            this.transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.fixedDeltaTime);
        }

        if (player.GetComponent<PlayerController>().isJumping)
            isOnPlatform = false;

         if (isOnPlatform) {
            // Calculate the new position of the player relative to the platform
            Vector3 newPosition = this.transform.position + platformOffset;

            // Update the player's position
            player.transform.position = newPosition;

            // Player movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);
            player.transform.position += movement * 3f * Time.fixedDeltaTime;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isOnPlatform = movingPlatform = true;
            platformOffset = player.transform.position - this.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Reset the platform offset
            platformOffset = Vector3.zero;
            // Unset the isOnPlatform flag
            isOnPlatform = false;
        }
    }
}