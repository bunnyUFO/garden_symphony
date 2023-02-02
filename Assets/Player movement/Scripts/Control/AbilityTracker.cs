using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTracker : MonoBehaviour
{
    [field: SerializeField] public int MaxUses { get; private set; }

    private List<string> abilitiesUsed = new List<string>();


    public bool TryAddAbility(string abilityName) 
    {
        if (abilitiesUsed.Count >= MaxUses || abilitiesUsed.Contains(abilityName)) {
            return false;
        }

        abilitiesUsed.Add(abilityName);
        return true;
    }

    public void Reset()
    {
        abilitiesUsed.Clear();
    }
}
