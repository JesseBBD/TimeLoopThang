using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
  public float spawnRate = 5.0f;
  float thisSpawnRate;
  public float spawnWidth = 10.0f;
  public string objectName = "Calculator";

  void Start()
  {
    StartCoroutine(SpawnObjects());
    thisSpawnRate = Random.Range(spawnRate / 2f, spawnRate);
  }

  IEnumerator SpawnObjects()
  {
    while (true)
    {
      float randomX = Random.Range(-spawnWidth / 2, spawnWidth / 2);
      Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, 0);
      Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

      if (objectName == "Paper")
      {
        Instantiate(Resources.Load("Prefabs/" + objectName + Random.Range(1, 7).ToString()), spawnPosition, randomRotation);
      }
      else
      {
        Instantiate(Resources.Load("Prefabs/" + objectName), spawnPosition, randomRotation);
      }
      yield return new WaitForSeconds(thisSpawnRate);
      thisSpawnRate = Random.Range(spawnRate / 2f, spawnRate);
    }
  }
}
