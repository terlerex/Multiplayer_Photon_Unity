using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    [SerializeField][Range(0,2)] private float speed = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rb.position;
        rb.position += Vector3.left * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}
