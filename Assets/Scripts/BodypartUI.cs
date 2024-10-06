using UnityEngine;
using UnityEngine.UI;

public class BodypartUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private IntData maxHealth;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    public void ColorChange(int health)
    {
        image.color = Color.HSVToRGB(((float)health)/maxHealth.value/4, 0.8f, 0.8f);
    }
}
