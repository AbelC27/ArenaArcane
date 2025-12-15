using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aceasta linie de obicei activeaza EndingCutscene sau WinScreen din UpgradeManager
        if (UpgradeManager.Instance != null)
        {
            UpgradeManager.Instance.BossDefeated();
        }
        else
        {
            // Fallback: Daca nu exista UpgradeManager, cautam manual EndingCutscene
            var ending = FindObjectOfType<EndingCutscene>();
            if (ending != null)
            {
                // Aici ar trebui sa ai un dialog de victorie definit in EndingCutscene sau sa il cauti
                // ending.StartEndingDialogue(victoryDialogue); // Daca ai referinta
            }
        }

        // Ascundem boss-ul
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.enabled = false;
        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        StartCoroutine(WinAndQuitSequence());
    }

    IEnumerator WinAndQuitSequence()
    {
        UnityEngine.Debug.Log("You Win!");

        // Oprim jocul (miscarea)
        Time.timeScale = 0f;

        // Asteptam 5 secunde sa vada ecranul de victorie
        yield return new WaitForSecondsRealtime(5f);

        UnityEngine.Debug.Log("Quitting...");
        UnityEngine.Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}