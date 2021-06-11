using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 75f;
        float healthPoints = -1f;
        ActionScheduler actionScheduler;
        Animator animator;
        NavMeshAgent navMeshAgent;
        BaseStats baseStats;
        bool isDead = false;
        //float playerDeathFadeOutTime = 3.0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            baseStats.onLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0 && !isDead)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
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

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) { return; }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            float regenerateHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenerateHealthPoints);
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