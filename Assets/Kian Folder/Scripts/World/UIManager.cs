using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider healthSlider;

    public Slider whipSlider;

    private void Start()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.health;
        whipSlider.maxValue = PlayerWhip.instance.oWhipHealCooldown;
        whipSlider.value = 0;
    }

    private void Update()
    {
        healthSlider.value = PlayerHealthController.instance.health;
        healthText.text = $"{PlayerHealthController.instance.health} / {PlayerHealthController.instance.maxHealth}";
        whipSlider.value = PlayerWhip.instance.whipHealCounter;
    }
}
