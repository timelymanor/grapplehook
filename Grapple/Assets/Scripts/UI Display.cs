using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMovementAdvanced pma;
    [SerializeField] private SwingingDone swinging;
    [SerializeField] private Health health;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Slider fuelGauge;
    [SerializeField] private Slider healthGauge;

    void Start()
    {
        fuelGauge.maxValue = swinging.fuelMax;
        healthGauge.maxValue = health.maxHealth;
    }

    void Update()
    {
        speedText.text = pma.moveMagnitude.ToString("0");
        fuelGauge.value = swinging.thrustFuel;
        healthGauge.value = health.health;
    }
}
