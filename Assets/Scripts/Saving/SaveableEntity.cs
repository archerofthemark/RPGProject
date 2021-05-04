using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";//System.Guid.NewGuid().ToString();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            print($"Capturing state for {GetUniqueIdentifier()}");
            return null;
        }

        public void RestoreState(object state)
        {
            print($"Restoring state for {GetUniqueIdentifier()}");
        }
    }
}