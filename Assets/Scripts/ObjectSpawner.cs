using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Reference to the prefab of the object you want to spawn
    public float moveSpeed = 5f; // Speed at which the objects move
    public float destroyDistance = 10f; // Distance at which the objects get destroyed
    public float spawnInterval = 3f; // Time interval between spawns

    private float timer; // Timer to track the spawn interval

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        // Start the timer
        timer -= Time.deltaTime;

        // Spawn object and reset the timer when it reaches 0 or less
        if (timer <= 0f)
        {
            SpawnObject();
            timer = spawnInterval;
        }

        // Move and check each spawned object
        foreach (Transform spawnedObject in transform)
        {
            // Move the object
            spawnedObject.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // Check if the object has reached the destroy distance
            if (spawnedObject.position.x >= destroyDistance)
            {
                // Destroy the object
                Destroy(spawnedObject.gameObject);
            }
        }
    }

    private void SpawnObject()
    {
        // Instantiate a new object
        GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        newObject.transform.SetParent(transform); // Set the spawner as the parent

        // Optionally, you can modify properties of the spawned object here
    }
}