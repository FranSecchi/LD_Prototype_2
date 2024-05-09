using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public float detectionRadius = 10f;
    public float groundDist = 10f;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    private CharacterController ch;

    private bool isFollowingPlayer = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, groundDist);
    }
    private void Start()
    {
        ch = GetComponent<CharacterController>();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward* 30f, Color.green);
        if (!isFollowingPlayer)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    player = collider.transform;
                    StartFollowingPlayer();
                    break;
                }
            }
        }
        else
        {
            FollowPlayer();
        }
    }

    void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Perform sphere cast to check for ground or wall
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, groundDist, -transform.up, groundDist, groundLayer);
            if (hits.Length > 0)
            {
                // Find the closest hit point
                RaycastHit closestHit = hits[0];
                float closestDistance = Mathf.Infinity;
                foreach (RaycastHit hit in hits)
                {
                    if(hit.normal == Vector3.up)
                    {
                        closestHit = hit;
                        break;
                    }
                    if (hit.distance < closestDistance)
                    {
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

                transform.rotation = Quaternion.LookRotation(playerDirection);
                // Move towards the point on the surface
                ch.Move(playerDirection * moveSpeed * Time.deltaTime);

                // Rotate towards the player
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            StartFollowingPlayer();
        }
    }
}
