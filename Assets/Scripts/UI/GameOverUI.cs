using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverScreen;
        [SerializeField]
        private TextMeshProUGUI secondsSurvivedUI;
        [SerializeField]
        private bool gameOver;
        private int count;
        private float currentTime;
        private float maxTime;

        public static event Action<bool> OnRestart;
        public PlayerController playerScript;


        void Start()
        {
            PlayerController.OnPlayerDeath += OnGameOver;
            PlayerController.OnCarrotCollected += IncrementCarrotCount;
            maxTime = 1;
        }

        void Update()
        {
            if (gameOver) return;
            /*
            currentTime += Time.deltaTime;
            if ( currentTime>maxTime)
            {
                currentTime = 0;
                count++;
            }
            secondsSurvivedUI.text = count.ToString();
            */
           
        }
        
        public void OnGameOver()
        {
            Debug.Log("You are in GameOver mode");
            gameOverScreen.SetActive(true);
            secondsSurvivedUI.text = count.ToString();
            gameOver = true;
            Time.timeScale = 0;

        }

        private void IncrementCarrotCount()
        {
            if (gameOver) return; // Si el juego ha terminado, no incrementes el contador.
            count++;
            secondsSurvivedUI.text = count.ToString(); // Actualiza el UI con el nuevo puntaje.
        }

        private void OnDestroy()
        {
            PlayerController.OnPlayerDeath -= OnGameOver;
            PlayerController.OnCarrotCollected -= IncrementCarrotCount; // Desuscríbete.
        }

        public void Restart()
        {
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            gameOver = false;
            // Reinicia el contador de puntos
            count = 0;  // Reinicia el contador
            secondsSurvivedUI.text = count.ToString(); // Actualiza la UI para mostrar el contador en 0

            playerScript = FindObjectOfType<PlayerController>();

            if (playerScript != null)
            {
                playerScript.life1.SetActive(true);
                playerScript.life2.SetActive(true);
                playerScript.life3.SetActive(true);
                playerScript.ResetLives(); // Método para restaurar el contador de vidas
            }
            OnRestart?.Invoke(gameOver);  // Llama al evento OnRestart si tienes otros scripts suscritos
            
        }

        public void Exit()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
