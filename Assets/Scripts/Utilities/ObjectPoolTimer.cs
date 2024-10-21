using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class ObjectPoolTimer : MonoBehaviour
    {
        [SerializeField] private float spawnInterval = 1f;  // Time between spawns
        [SerializeField] private float timeSinceLastSpawn;
        [SerializeField] private Transform targetPos;
        private float _posX;
        private float _posY;

        private const float SPAWN_POS = 5f;

        void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                // Set _posX to a random value between -SPAWN_POS and SPAWN_POS
                _posX = Random.Range(-SPAWN_POS, SPAWN_POS);  // Add this line

                // Apply clamping
                _posX = Mathf.Clamp(_posX, -SPAWN_POS, SPAWN_POS);
                
                // Set _posY based on target position
                _posY = targetPos.position.y + -SPAWN_POS; 

                // Spawn object at the clamped position
                ObjectPooler.Instance.SpawnObjectFromPool(new Vector3(_posX, _posY, 0));

                // Reset time since last spawn
                timeSinceLastSpawn = 0f;
            }
        }
    }
}
