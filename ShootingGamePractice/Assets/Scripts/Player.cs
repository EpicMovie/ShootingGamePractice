using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour
{
    void Start()
    {
        Controller = GetComponent<PlayerController>();
        ViewCamera = Camera.main;
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * MoveSpeed;

        Controller.Move(moveVelocity);

        Ray ray = ViewCamera.ScreenPointToRay (Input.mousePosition);

        Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float rayDist))
        {
            Vector3 point = ray.GetPoint(rayDist);

            Controller.LookAt(point);
        }
    }

    public float MoveSpeed = 5;

    protected Camera ViewCamera;
    protected PlayerController Controller;
}
