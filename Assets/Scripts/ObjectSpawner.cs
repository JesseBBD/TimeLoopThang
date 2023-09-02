using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
  public float spawnRate = 1.0f;
  public float spawnWidth = 10.0f;
  public string objectName = "Calculator";

  void Start()
  {
    StartCoroutine(SpawnObjects());
  }

  IEnumerator SpawnObjects()
  {
    while (true)
    {
      float randomX = Random.Range(-spawnWidth / 2, spawnWidth / 2);
      Vector3 spawnPosition = new Vector3(randomX, Camera.main.orthographicSize + 1, 0);
      if (objectName == "Paper")
      {
        Instantiate(Resources.Load("Prefabs/" + objectName + Random.Range(1, 7).ToString()), spawnPosition, Quaternion.identity);
      }
      else
      {
        Instantiate(Resources.Load("Prefabs/" + objectName), spawnPosition, Quaternion.identity);
      }
      yield return new WaitForSeconds(1.0f / spawnRate);
    }
  }
}
