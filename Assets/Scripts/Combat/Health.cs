using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        Animator animator;
        bool isDead = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0 && !isDead)
            {
                isDead = true;
                Die();
            }
        }

        public void Die()
        {
            animator.SetTrigger("die");
        }
    }
}