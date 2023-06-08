using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerTouchController : MonoBehaviour {

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 v = moveAction.action.ReadValue<Vector2>();
        transform.Translate(v * speed * Time.deltaTime);
    }
}