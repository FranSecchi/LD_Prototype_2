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
    public float damageCD = 0.5f;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private CharacterController ch;
    private float diff = 0f;
    private float elapsed = 0f;
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, groundDist, -transform.up, groundDist, groundLayer);
        if (hits.Length > 0)
        {
            // Find the closest hit point
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
                    closestHit = hit;
                    closestDistance = hit.distance;
                }
            }

            Vector3 direction = (player.position - diff * Vector3.up - transform.position);
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
    void Update()
    {
        if (contr.IsDead) Die();
        FollowPlayer();
        elapsed += Time.deltaTime;
    }

    void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    void FollowPlayer()
    {
        if (isFollowingPlayer)
        {
            Vector3 direction = (player.position - diff*Vector3.up - transform.position);
            if (direction.magnitude < ch.radius + 0.8f && elapsed > damageCD)
            {
                player.GetComponent<IDamageable>().TakeDamage(damage, transform);
                elapsed = 0f;
            }
            direction.Normalize();
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(player==null) player = other.transform;
            diff = player.GetComponent<CharacterController>().height / 2 - ch.height / 2;
            RaycastHit hit;
            // Use the adjusted starting position for the raycast
            bool b = Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, detectionRadius, wallLayer);
            Debug.DrawRay(transform.position, (player.position - transform.position), Color.magenta);
            if (b && hit.collider.CompareTag("Player"))
            {
                isFollowingPlayer = true;
            }
            else isFollowingPlayer = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isFollowingPlayer = false;
    }

    public void TakeDamage(float amount, Transform actor)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
