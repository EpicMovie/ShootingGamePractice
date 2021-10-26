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
        float moveDistance = speed * Time.deltaTime;

        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * moveDistance);   
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    protected void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();

        if(damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }

        GameObject.Destroy(gameObject);
    }

    public LayerMask collisionMask;

    protected float speed = 10;
    protected float damage = 1;
}
