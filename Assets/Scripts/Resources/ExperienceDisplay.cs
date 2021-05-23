using System;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    Text experienceText;
    Experience experience;

    private void Awake()
    {
        experienceText = GetComponent<Text>();
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }

    private void Update()
    {
        experienceText.text = String.Format("XP: {0:0}", experience.GetExperience());
    }
}
