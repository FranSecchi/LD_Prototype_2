using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumpider : MonoBehaviour, IDamageable
{
    public GameObject cum;
    public float moveSpeed = 3f;
    public float shootRadius = 10f;
    public float groundDist = 10f;
    public float damage = 10f;
    public float fireRate = 10f;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Transform player;
    private CharacterController ch;
    private float diff = 0f;
    private bool isFollowingPlayer = false;
    public bool gizmos = false;
    private float elapsed = 0f;
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundDist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
    private void Start()
    {
        elapsed = fireRate;
        ch = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (elapsed < fireRate) elapsed += Time.deltaTime;
        if (isFollowingPlayer)
            FollowPlayer();
    }

    private void Shoot()
    {
        if (elapsed >= fireRate)
        {
            
            RaycastHit hit;
            // Use the adjusted starting position for the raycast
            bool b = Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, shootRadius, wallLayer | playerLayer);
            if (b && hit.collider.CompareTag("Player"))
            {
                Instantiate(cum, transform.position, Quaternion.identity);
                elapsed = 0f;
            }
        }
    }


    void FollowPlayer()
    {
        Vector3 direction = (player.position - diff * Vector3.up - transform.position);
        direction.Normalize();
        // Perform sphere cast to check for ground or wall
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, groundDist, -transform.up, groundDist, groundLayer);
        if (hits.Length > 0)
        {
            // Find the closest hit point
            bool b = false;
            RaycastHit closestHit = hits[0];
            float closestDistance = Mathf.Infinity;
            foreach (RaycastHit hit in hits)
            {
                if (hit.normal == Vector3.up)
                {
                    closestHit = hit;
                    break;
                }
                if (hit.distance < closestDistance)
                {
                    b = true;
                    closestHit = hit;
                    closestDistance = hit.distance;
                }
            }

            // Calculate movement direction towards the player
            Vector3 playerDirection = direction;
            Vector3 normal = transform.position - closestHit.collider.ClosestPointOnBounds(transform.position);
            // If the spider is on the ground, project the direction onto the surface normal
            if (closestHit.normal != Vector3.zero)
            {
                playerDirection = Vector3.ProjectOnPlane(playerDirection, normal.normalized).normalized;
            }

            transform.rotation = Quaternion.LookRotation(playerDirection, b ? normal.normalized : transform.up);
            if (direction.magnitude < shootRadius)
            {
                Shoot();
                return;
            }
            // Move towards the point on the surface
            ch.Move(playerDirection * moveSpeed * Time.deltaTime);
            // Rotate towards the player
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowingPlayer = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null) player = other.transform;
            diff = player.GetComponent<CharacterController>().height / 2 - ch.height / 2;
            //RaycastHit hit;
            //// Use the adjusted starting position for the raycast
            //bool b = Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, detectionRadius, wallLayer);
            //if (b && hit.collider.CompareTag("Player"))
            //{
            isFollowingPlayer = true;
            //}
            //else isFollowingPlayer = false;
        }
    }

    public void TakeDamage(float amount, Transform actor)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    public void PerformRaycastAndUpdatePosition()
    {
        RaycastHit hit;
        // Use the adjusted starting position for the raycast
        bool b = Physics.Raycast(transform.position, -transform.up, out hit, 10f, wallLayer | groundLayer);
        if (b)
        {
            transform.position = hit.point + transform.up * 0.15f;
        }
    }
}
