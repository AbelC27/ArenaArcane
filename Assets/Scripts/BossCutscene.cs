using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;        // Drag your Camera (child of player) here
    public TextMeshProUGUI bossNameText;     // Text: "THE NECROMANCER"

    [Header("Settings")]
    public float cameraSpeed = 5f;

    private Transform originalParent;        // To remember the Player object
    private Vector3 originalLocalPos;        // To remember exactly where the camera sits

    public void StartBossSequence(GameObject boss, Transform player)
    {
        StartCoroutine(CutsceneRoutine(boss, player));
    }

    IEnumerator CutsceneRoutine(GameObject boss, Transform player)
    {
        // 1. OPRIM BOSS-UL (Ca să nu tragă în tine cât te uiți la el)
        // Încearcă să găsească scripturile de tragere și mișcare
        var bossShooting = boss.GetComponent<BossShooting>();
        var bossAI = boss.GetComponent<EnemyAI>(); // Sau scriptul tău de mișcare pt boss

        if (bossShooting) bossShooting.enabled = false;
        if (bossAI) bossAI.enabled = false;

        // 2. SAVE & DETACH CAMERA
        originalParent = cameraTransform.parent;
        originalLocalPos = cameraTransform.localPosition;
        cameraTransform.parent = null;

        // 3. FREEZE PLAYER
        var playerMove = player.GetComponent<PlayerMovement>();
        var weaponCtrl = player.GetComponent<WeaponController>();
        if (playerMove) playerMove.enabled = false;
        if (weaponCtrl) weaponCtrl.enabled = false;

        // 4. SHOW TITLE
        if (bossNameText != null)
        {
            bossNameText.gameObject.SetActive(true);
            bossNameText.text = "GRAND WARDEN";
        }

        // 5. PAN CAMERA TO BOSS
        float timer = 0f;
        Vector3 bossTarget = new Vector3(boss.transform.position.x, boss.transform.position.y, -10);

        // -- AM REDUS TIMPUL DE PANNING PENTRU A FI MAI RAPID (opțional) --
        while (timer < 1.5f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, bossTarget, Time.deltaTime * cameraSpeed);
            timer += Time.deltaTime;
            yield return null;
        }

        // Wait a moment to look at the boss
        yield return new WaitForSeconds(1.0f);

        // 6. PAN BACK TO PLAYER
        Vector3 playerTarget = new Vector3(player.position.x, player.position.y, -10);
        timer = 0f;
        while (timer < 1.0f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTarget, Time.deltaTime * cameraSpeed * 2);
            timer += Time.deltaTime;
            yield return null;
        }

        // 7. RE-ATTACH CAMERA & RESUME PLAYER
        if (originalParent != null)
        {
            cameraTransform.parent = originalParent;
            cameraTransform.localPosition = originalLocalPos;
        }

        if (playerMove) playerMove.enabled = true;
        if (weaponCtrl) weaponCtrl.enabled = true;

        if (bossNameText != null) bossNameText.gameObject.SetActive(false);

        // 8. PORNEȘTE BOSS-UL LA FINAL (ACUM ÎNCEPE LUPTA)
        if (bossShooting) bossShooting.enabled = true;
        if (bossAI) bossAI.enabled = true;

        UnityEngine.Debug.Log("FIGHT START!");
    }
}