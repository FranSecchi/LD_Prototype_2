using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IDamageable
{
    public float moveSpeed = 3f;
    private Transform player;
    public float detectionRadius = 10f;
    public float groundDist = 10f;
    public float damage = 10f;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    private CharacterController ch;
    private float diff = 0f;
    private bool isFollowingPlayer = false;
    private FPSController contr;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, groundDist);
    }
    private void Start()
    {
        contr = FindObjectOfType<FPSController>();
        ch = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (contr.IsDead) Die();
        Debug.DrawRay(transform.position, transform.forward* 30f, Color.green);
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            isFollowingPlayer = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.transform;
                diff = player.GetComponent<CharacterController>().height / 2 - ch.height / 2;
                RaycastHit hit;
                bool b = Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, detectionRadius);
                if (b && hit.collider.CompareTag("Player"))
                {
                    StartFollowingPlayer();
                }
                break;
            }
        }
        FollowPlayer();
    }

    void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    void FollowPlayer()
    {
        if (isFollowingPlayer)
        {
            Vector3 direction = (player.position - diff*Vector3.up - transform.position).normalized;

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
            other.transform.GetComponent<IDamageable>().TakeDamage(damage, Vector3.zero);
        }
    }

    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
