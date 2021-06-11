using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Text levelText;
        BaseStats baseStats;

        private void Awake()
        {
            levelText = GetComponent<Text>();
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            levelText.text = String.Format("Level: {0:0}", baseStats.GetLevel());
        }
    }
}