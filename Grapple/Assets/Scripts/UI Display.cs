using TMPro;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMovementAdvanced pma;

    [SerializeField] private TextMeshProUGUI speedText;

    void Start()
    {
    }

    void Update()
    {
        speedText.text = pma.moveMagnitude.ToString("F2");
    }
}
