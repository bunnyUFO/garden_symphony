using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChargeIcon : MonoBehaviour
{
    [SerializeField] Image chargeImage;
    
    public void ToggleCharge(bool isAvailable)
    {
        chargeImage.gameObject.SetActive(isAvailable);
    }
}
