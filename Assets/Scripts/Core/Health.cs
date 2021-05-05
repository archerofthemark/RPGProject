using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        ActionScheduler actionScheduler;
        Animator animator;
        NavMeshAgent navMeshAgent;
        bool isDead = false;
        //float playerDeathFadeOutTime = 3.0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
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
                Die();
            }
        }

        public void Die()
        {
            //if(CompareTag("Player")) { 
            //    Fader fader = FindObjectOfType<Fader>();
            //    StartCoroutine(fader.FadeOut(playerDeathFadeOutTime)); 
            //}
            if(isDead) { return; }
            isDead = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
            //navMeshAgent.enabled = false;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}