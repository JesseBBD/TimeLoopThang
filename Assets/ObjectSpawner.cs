using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
  public GameObject fallingObjectPrefab;  // Assign your prefab here in the inspector
  public float spawnRate = 1.0f;  // How many objects per second
  public float spawnWidth = 10.0f;  // Width of the spawning area
  public string objectName =  "Calculator";

  void Start()
  {
    StartCoroutine(SpawnObjects());
  }

  IEnumerator SpawnObjects()
  {
    while (true)
    {
      // Generate a random X position within the spawnWidth
      float randomX = Random.Range(-spawnWidth / 2, spawnWidth / 2);

      // Use the random X position and a Y position that is just above the camera's view
      Vector3 spawnPosition = new Vector3(randomX, Camera.main.orthographicSize + 1, 0);

      // Instantiate the object at the spawnPosition
      Instantiate(Resources.Load("Prefabs/"+objectName), spawnPosition, Quaternion.identity);

      // Wait for the next spawn
      yield return new WaitForSeconds(1.0f / spawnRate);
    }
  }
}
