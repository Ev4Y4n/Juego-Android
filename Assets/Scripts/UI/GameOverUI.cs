using Player;
using TMPro;
using UnityEngine;

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

        void Start()
        {
            FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
        }

        void Update()
        {
            secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
            if (!gameOver) return;
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                //SceneManager.LoadScene("");
            }
        }
        public void OnGameOver()
        {
            gameOverScreen.SetActive(true);
            secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
            gameOver = true;
        }
    }
}

