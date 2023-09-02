using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


  void Update()
  {
    float rotationAmount = Time.deltaTime * speed;
    transform.Rotate(0, 0, rotationAmount);
  }
}
