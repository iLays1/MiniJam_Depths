using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI fuelText;
    public Player player;

    private void Awake()
    {
        player.OnValueChange.AddListener(UpdateUI);
    }

    void UpdateUI()
    {
        slider.maxValue = player.maxFuel;
        slider.DOKill();
        slider.DOValue(player.fuel, 0.3f).SetEase(Ease.OutBack);
        fuelText.text = player.fuel.ToString();
    }
}
