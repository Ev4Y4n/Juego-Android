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

        void Start()
        {
            PlayerController.OnPlayerDeath += OnGameOver;
            maxTime = 1;
        }

        void Update()
        {
            if (gameOver) return;
            currentTime += Time.deltaTime;
            if ( currentTime>maxTime)
            {
                currentTime = 0;
                count++;
            }
            secondsSurvivedUI.text = count.ToString();
           

            
        }
        
        public void OnGameOver()
        {
            gameOverScreen.SetActive(true);
            secondsSurvivedUI.text = count.ToString();
            gameOver = true;
            Time.timeScale = 0;

        }

        public void Restart()
        {
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            gameOver = false;
            count = 0;
            OnRestart?.Invoke(gameOver);

        }

        public void Exit()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
