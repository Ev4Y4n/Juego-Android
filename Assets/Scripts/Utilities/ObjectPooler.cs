using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Utilities
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance { get; private set; }

        // List of prefabs to spawn from
        [SerializeField] private GameObject[] objectPrefabs;

        // Object pool size
        [SerializeField] private int poolSize = 15;

        // The pool of inactive objects
        private List<GameObject> _objectPool;

        // Minimum and maximum scale factors for random scaling
        [SerializeField] private Vector2 scaleRangeX = new(1f, 3f);  // Random scale for X
        [SerializeField] private Vector2 scaleRangeY = new(1f, 3f);  // Random scale for Y

        private void OnEnable()
        {
            Instance = this;
        }

        void Start()
        {
            // Initialize the object pool
            _objectPool = new List<GameObject>();

            // Populate the pool with objects
            for (var i = 0; i < poolSize; i++)
            {
                // Pick a random prefab from the array
                var obj = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)]);

                // Disable the object and add to the pool
                obj.SetActive(false);
                _objectPool.Add(obj);
            }
        }

        // Method to get an object from the pool
        private GameObject GetPooledObject()
        {
            // Find an inactive object in the pool using Linq 
            return _objectPool.FirstOrDefault(obj => !obj.activeInHierarchy);
        }

        // Method to spawn an object from the pool at a specific position
        public void SpawnObjectFromPool(Vector3 spawnPosition)
        {
            // Get an inactive object from the pool
            var pooledObject = GetPooledObject();

            if (pooledObject != null)
            {
                // Set object to the spawn position
                pooledObject.transform.position = spawnPosition;

                // Apply random scaling to the object on either X or Y axis
                Vector3 randomScale = Vector3.one;

                // Randomly decide to scale on X or Y
                if (Random.value > 0.5f)
                    // Random scale on X axis
                    randomScale.x = Random.Range(scaleRangeX.x, scaleRangeX.y);
                else
                    // Random scale on Y axis
                    randomScale.y = Random.Range(scaleRangeY.x, scaleRangeY.y);
                // Apply the scale to the object
                pooledObject.transform.localScale = randomScale;

                // Activate the object
                pooledObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No available objects in the pool! Consider increasing the pool size.");
            }
        }
    }
}
