using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField] private Transform[] waypoints;  // An array of waypoints the platform will move between
    public float speed = 3f;      // The speed at which the platform moves
    private int currentWaypoint = 0;  // The index of the current waypoint
    private bool movingPlatform = false;
    private Vector3 platformOffset;
    private GameObject playerParent;

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
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerParent = collision.transform.parent.gameObject;
            movingPlatform = true;
            SoundManager.Instance.PlayMovingRockSound();
            platformOffset = collision.gameObject.transform.position - this.transform.position;
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            SoundManager.Instance.StopFXSound();
            // Reset the platform offset
            platformOffset = Vector3.zero;
            collision.gameObject.transform.SetParent(playerParent.transform);
        }
    }
}