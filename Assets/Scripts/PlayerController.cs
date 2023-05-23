using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private RigidBody2D rb;

    private void Awake() {
        rb = GetComponent<RigidBody2D>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(1, 0.5f);
    }
}
