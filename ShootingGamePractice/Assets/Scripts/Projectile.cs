using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);   
    }

    protected float speed = 10;
}
