using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody grabberRB;
    private const float G = 667.4f;
    public float speed;

    public Rigidbody grabbleRB;

    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grabbleRB.MovePosition(Vector3.Lerp(grabbleRB.position, grabberRB.position, Time.deltaTime * speed));
    }
}
