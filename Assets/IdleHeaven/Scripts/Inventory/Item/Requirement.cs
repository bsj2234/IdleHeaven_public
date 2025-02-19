using UnityEngine;

namespace IdleHeaven
{
    public interface IRequirement
    {
        bool IsSatisfied(GameObject character);
    }

    [System.Serializable]
    public class LevelRequirement : ScriptableObject, IRequirement
    {
        [SerializeField] private int _requiredLevel;

        public bool IsSatisfied(GameObject character)
        {
            CharacterStats stats = character.GetComponent<CharacterStats>();
            return false;
        }
    }
}
