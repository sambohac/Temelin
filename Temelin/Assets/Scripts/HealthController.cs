using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    TMP_Text healthText;

    public void UpdateHealth(int  health)
    {
        healthText.text = $"{health}hp";
    }
}
