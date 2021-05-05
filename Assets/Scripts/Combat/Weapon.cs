using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}