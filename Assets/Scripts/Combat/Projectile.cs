using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float maxRange = 20;
    [SerializeField] bool isHoming = false;

    Health target = null;
    float damage = 0;
    Vector3 startPosition;
    

    void Start()
    {
        startPosition = transform.position;
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if(target == null) { return; }
        
        if(isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        if(Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target.IsDead()) { return; }
        if(other.GetComponent<Health>() == target)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
