using RPG.Resources;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text healthText;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                healthText.text = String.Format("Enemy: N/A");
                return;
            }
            Health health = fighter.GetTarget();
            
                healthText.text = String.Format("Enemy: {0:0}%", health.GetPercentage());
        }
    }
}