using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float downwardSpeed = 5f;      // Speed at which player moves down
        [SerializeField] private float horizontalSpeed = 10f;   // Speed at which player moves horizontally
        [SerializeField] private float screenLimitX; //Limit on X-axis
        
        public event Action OnPlayerDeath;
    
        void Update()
        {
            transform.Translate(Vector3.down * (downwardSpeed * Time.deltaTime));
            HandleHorizontalInput();
        }
        
        private void HandleHorizontalInput()
        {
            var horizontalInput = 0f;
            
            // In the Unity Editor, use keyboard input for testing
            if (Application.isEditor)
            {
                horizontalInput = Input.GetAxis("Horizontal");
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                // On Android, use the accelerometer (tilt) for horizontal movement
                horizontalInput = Input.acceleration.x;
            }
            // Get current position
            var position = transform.position;

            // Input handling
            position.x += horizontalInput * horizontalSpeed * Time.deltaTime;

            // Clamp the position within screen bounds (assuming -X and +X as limits)
            position.x = Mathf.Clamp(position.x, -screenLimitX, screenLimitX);

            // Apply the position back to the player
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Obstacle")) return;
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}

