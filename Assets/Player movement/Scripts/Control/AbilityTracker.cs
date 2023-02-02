using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTracker : MonoBehaviour
{
    [field: SerializeField] public int MaxUses { get; private set; }

    [Header("Components")]
    [SerializeField] GameObject abilityTrackerPanel;
    [SerializeField] GameObject abilityChargeIconPrefab;

    private List<string> abilitiesUsed = new List<string>();
    private List<AbilityChargeIcon> abilityChargeIcons = new List<AbilityChargeIcon>();

    private void Awake()
    {
        if (abilityTrackerPanel != null && abilityChargeIconPrefab != null) {
            for (int i = 0; i < MaxUses; i++) {
                abilityChargeIcons.Add(Instantiate(abilityChargeIconPrefab, abilityTrackerPanel.transform).GetComponent<AbilityChargeIcon>());
            }
        }
    }

    public bool TryAddAbility(string abilityName) 
    {
        if (abilitiesUsed.Count >= MaxUses || abilitiesUsed.Contains(abilityName)) {
            return false;
        }

        abilityChargeIcons[abilitiesUsed.Count].ToggleCharge(false);
        abilitiesUsed.Add(abilityName);
        return true;
    }

    public void Reset()
    {
        for (int i = 0; i < MaxUses; i++) {
                abilityChargeIcons[i].ToggleCharge(true);
            }
        abilitiesUsed.Clear();
    }
}
