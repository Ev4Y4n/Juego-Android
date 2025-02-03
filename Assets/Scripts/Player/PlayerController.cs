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
        public static event Action OnCarrotCollected;
        public static event Action<bool> OnHurt;

        public GameObject life1, life2, life3;
        private int lifeCounter=3;

        private void Start()
        {
            GameOverUI.OnRestart += GameOverUI_OnRestart;
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
            //ResetLives();
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

        public void ResetLives()
        {
            lifeCounter = 3;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Te has chocado");
                OnHurt?.Invoke(true);
                if(lifeCounter==3)
                {
                    life1.SetActive(false);
                    lifeCounter--;
                }else if (lifeCounter == 2)
                {
                    life2.SetActive(false);
                    lifeCounter--;
                }else if (lifeCounter == 1)
                {
                    life3.SetActive(false);
                    lifeCounter--;
                    if (lifeCounter == 0)
                    {
                        OnPlayerDeath?.Invoke();
                        gameOver = true;
                    }
                }
            }
            
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Te has chocado");
                OnHurt?.Invoke(false);
                //OnPlayerDeath?.Invoke();
                //gameOver = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Carrot"))
            {
                Debug.Log("Has recogido una zanahoria");
                OnCarrotCollected?.Invoke();
                collision.gameObject.SetActive(false);
            }
        }


    }
}

