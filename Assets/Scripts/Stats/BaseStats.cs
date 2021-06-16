using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifiers(stat) * (1 + GetPercentageModifiers(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if(currentLevel < 1)
            { 
                currentLevel = CalculateLevel(); 
            }
            return currentLevel;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(experience == null) { return startingLevel; }

            float currentExperience = GetComponent<Experience>().GetExperience();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level <= penultimateLevel; level++)
            {
                float experienceToLevel = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(experienceToLevel > currentExperience)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
        private float GetAdditiveModifiers(Stat stat)
        {
            if(!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifiers(Stat stat)
        {
            if(!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform.parent);
        }
    }
}