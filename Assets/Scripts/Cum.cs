using UnityEngine;

internal class Cum : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;


    public Vector3 Target { get; internal set; }
    private void Start()
    {
        Target = FindObjectOfType<FPSController>().transform.position;
    }
    private void FixedUpdate()
    {
        transform.forward = Target - transform.position;
        transform.position += ((Target - transform.position).normalized * speed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, Target) < 0.1f) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            IDamageable dmg = other.transform.GetComponent<IDamageable>();
            dmg.TakeDamage(damage, null);
            Destroy(gameObject);
        }
    }
}