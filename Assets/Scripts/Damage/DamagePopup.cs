using UnityEngine;
using TMPro;
using System;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float moveYSpeed = 1f;
    public float lifetime = 1f;
    public float fadeSpeed = 2f;

    private float timer;
    private Color textColor;

    public void Setup(float damage)
    {
        textMesh.text = damage.ToString();
        textColor = textMesh.color;
        timer = lifetime;
    }

    void Update()
    {
        // Float up
        transform.position += new Vector3(0, moveYSpeed * Time.deltaTime, 0);

        // Countdown
        timer -= Time.deltaTime;

        // Fade out near the end
        if (timer < lifetime / 2f)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
