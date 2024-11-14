using UnityEngine;
using Utilities;
using UI;

namespace GameMechanic
{
    public class Spawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject obstacle;
        [Header("Time")]
        [SerializeField] private float nextSpawnTime;
        [SerializeField] private Vector2 secondBtwSpawnMinMax;
        [Header("ScreenSize")]
        [SerializeField] private Vector2 screenHalfSizeWorldUnit;
        [Header("SpawnSize")]
        [SerializeField] private Vector2 spawnSizeMinMax;
        [SerializeField] private int spawnSize;
        [SerializeField] private float spawnAngleMinMax;
        [SerializeField] private float spawnAngle;
        private bool gameOver;
        void Start()
        {
            screenHalfSizeWorldUnit = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
            GameOverUI.OnRestart += GameOverUI_OnRestart;
        }

        private void GameOverUI_OnRestart(bool isRestart)
        {
            gameOver = isRestart;
            
        }
        void Update()
        {
            if (gameOver) return;
            if (!(Time.time > nextSpawnTime)) return;
            float secondBtwSpawn = Mathf.Lerp(secondBtwSpawnMinMax.y, secondBtwSpawnMinMax.x, GraduallyDifficult.GetDifficultPercent());
            nextSpawnTime = Time.time + secondBtwSpawn;
            spawnSize = Random.Range(1, 5);
            spawnAngle = Random.Range(-spawnAngleMinMax, spawnAngleMinMax);
            RandomSpawnPrefab();
        }
        private void RandomSpawnPrefab()
        {
            var randomPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnit.x, screenHalfSizeWorldUnit.x), transform.position.y);

            var newRandomBlock = Instantiate(obstacle, randomPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newRandomBlock.transform.localScale = new Vector3(spawnSize,1);
            newRandomBlock.transform.parent = transform;
        }
    }
}
