using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTable : MonoBehaviour
{
    public float rotateTime = 1.0f;      // Time taken to rotate 90 degrees
    public float waitTime = 2.0f;        // Time to wait before rotating back
    public float returnTime = 1.0f;      // Time taken to return to original position

    private Quaternion originalRotation;
    private Quaternion rotatedPosition;

    void Start()
    {
        originalRotation = transform.rotation;
        rotatedPosition = Quaternion.Euler(0, 0, transform.eulerAngles.z + 90);
        StartCoroutine(RotatePlatform());
        
    }

  IEnumerator RotatePlatform()
  {
    while (true)
    {
      // Rotate to 90 degrees
      float startTime = Time.time;
      while (Time.time < startTime + rotateTime)
      {
        float t = (Time.time - startTime) / rotateTime;
        transform.rotation = Quaternion.Lerp(originalRotation, rotatedPosition, t);
        yield return null;
      }
      transform.rotation = rotatedPosition;

      // Wait
      yield return new WaitForSeconds(waitTime);

      // Rotate back to original position
      startTime = Time.time;
      while (Time.time < startTime + returnTime)
      {
        float t = (Time.time - startTime) / returnTime;
        transform.rotation = Quaternion.Lerp(rotatedPosition, originalRotation, t);
        yield return null;
      }
      transform.rotation = originalRotation;

      // Wait
      yield return new WaitForSeconds(waitTime);
    }
  }

  // Update is called once per frame
  void Update()
    {
        
    }
}
