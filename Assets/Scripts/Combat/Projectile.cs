using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float maxRange = 20;

    Health target = null;
    float damage = 0;
    bool targetSet = false;
    Vector3 startPosition;
    

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if(target == null) { return; }
        if(!targetSet)
        {            
            transform.LookAt(GetAimLocation());
            targetSet = true;
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
        if(other.GetComponent<Health>() == target)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
