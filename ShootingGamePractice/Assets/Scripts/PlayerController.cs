using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 InVelocity)
    {
        Velocity = InVelocity;
    }

    public void LookAt(Vector3 InLookPoint)
    {
        // List<Vector3> vectorList = new List<Vector3>();
        Vector3 heightCorrectness = new Vector3(InLookPoint.x, transform.position.y, InLookPoint.z);

        transform.LookAt(heightCorrectness);
    }

    public void FixedUpdate()
    {
        MyRigidBody.MovePosition(MyRigidBody.position + Velocity * Time.fixedDeltaTime);
    }

    protected Vector3 Velocity;

    protected Rigidbody MyRigidBody;
}
