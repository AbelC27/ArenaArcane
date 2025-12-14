using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class DamagePopup : MonoBehaviour
{
    // Settings you can change in the Inspector
    public float moveSpeed = 2f;
    public float disappearSpeed = 3f;
    public float lifeTime = 1f;

    private TextMeshPro textMesh;
    private Color textColor;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    // Call this to set the damage number
    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

    void Update()
    {
        // 1. Float Up
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // 2. Timer to start fading
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            // 3. Fade Out
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            // 4. Destroy when invisible
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}