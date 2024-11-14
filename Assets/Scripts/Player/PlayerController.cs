using System;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float downwardSpeed = 5f;      // Speed at which player moves down
        [SerializeField] private float horizontalSpeed = 10f;   // Speed at which player moves horizontally
        [SerializeField] private float screenLimitX; //Limit on X-axis
        private bool gameOver;

        public Vector2 initialPosition = new Vector2(0, 0); //The player starts on the position 0, 0

        public static event Action OnPlayerDeath;


        private void Start()
        {
            GameOverUI.OnRestart += GameOverUI_OnRestart;
        }

        private void GameOverUI_OnRestart(bool isRestart)
        {
            gameOver = isRestart;
            transform.position = new Vector2(0, 0);
        }

        void Update()
        {
            if (gameOver) return;
            transform.Translate(Vector3.down * (downwardSpeed * Time.deltaTime));
            HandleHorizontalInput();
            
            
            //transform.position = initialPosition;
        }

        private void HandleHorizontalInput()
        {
            var horizontalInput = 0f;
            
            // In the Unity Editor, use keyboard input for testing
            
            if (Application.isEditor)
            {
                horizontalInput = Input.GetAxis("Horizontal");
            }
            
            //horizontalInput = Input.acceleration.x;
          
            else if (Application.platform == RuntimePlatform.Android)
            {
                // On Android, use the accelerometer (tilt) for horizontal movement
                
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
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Te has chocado");
                OnPlayerDeath?.Invoke();
                gameOver = true;
            }
            
            
        }
    }
}

