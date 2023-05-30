using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed; // Adjust this value to control the speed of the parallax effect

    private Camera mainCamera;
    private float screenWidth;
    private float spriteWidth;
    private float previousPlayerX;
    public Transform player;

    private void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        // Calculate the width of the sprites
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;

        // Initialize the previous player X position
        previousPlayerX = player.position.x;
    }

    private void Update() {
        // Calculate the player's movement direction
        float playerDirection = Mathf.Sign(player.position.x - previousPlayerX);

        // Calculate the parallax delta based on the player's movement direction
        float parallaxDelta = parallaxSpeed * Time.deltaTime * playerDirection;

        if (Mathf.Abs(player.position.x - previousPlayerX) > 0.01f) {
            // Move each child object
            foreach (Transform child in transform) {
                // Move the sprite
                Vector3 newPosition = child.position;
                newPosition.x -= parallaxDelta;
                child.position = newPosition;
                
                // Wrap the sprite to the opposite side of the screen if it goes offscreen
                if (playerDirection > 0 && child.position.x + spriteWidth < mainCamera.transform.position.x - screenWidth / 2f) {
                    newPosition.x += spriteWidth * transform.childCount;
                    child.position = newPosition;
                } else if (playerDirection < 0 && child.position.x - spriteWidth > mainCamera.transform.position.x + screenWidth / 2f) {
                    newPosition.x -= spriteWidth * transform.childCount;
                    child.position = newPosition;
                }
            }
        }
        // Update the previous player X position
        previousPlayerX = player.position.x;
    }
}