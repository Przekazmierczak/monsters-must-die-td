using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image barImage;
    private RectTransform rt;

    private Enemy enemy;
    float width;
    float height;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();

        barImage = GetComponent<Image>();
        rt = barImage.GetComponent<RectTransform>();

        width = rt.rect.width;
        height = rt.rect.height;
    }

    void Update()
    {   
        barImage.fillAmount = enemy.health / enemy.maxHealth;
    }
}
