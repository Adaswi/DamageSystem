using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private IntData maxHealth;

    private void Start()
    {
        SetMaxHealth(maxHealth.value);
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
