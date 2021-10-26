using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof(GunController))]
public class Player : MonoBehaviour
{
    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();

        viewCamera = Camera.main;
    }

    void Update()
    {
        // Handle Input
        Move();
        Look();
        Shoot();
    }

    protected void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        controller.Move(moveVelocity);
    }

    protected void Look()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float rayDist))
        {
            Vector3 point = ray.GetPoint(rayDist);

            controller.LookAt(point);
        }
    }

    protected void Shoot()
    {
        const int LEFT_MOUSE_BUTTON = 0;

        if(Input.GetMouseButton(LEFT_MOUSE_BUTTON))
        {
            gunController.Shoot();
        }
    }

    public float moveSpeed = 5;

    protected Camera viewCamera;
    protected PlayerController controller;
    protected GunController gunController;
}
