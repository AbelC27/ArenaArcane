using UnityEngine;
using System.Collections;
using TMPro;

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
        // 1. SAVE & DETACH CAMERA
        // We save who the parent was (the Player) so we can go back later
        originalParent = cameraTransform.parent;
        originalLocalPos = cameraTransform.localPosition;

        // Detach the camera so it can move freely to the boss
        cameraTransform.parent = null;

        // 2. FREEZE PLAYER
        var playerMove = player.GetComponent<PlayerMovement>();
        var weaponCtrl = player.GetComponent<WeaponController>();
        if (playerMove) playerMove.enabled = false;
        if (weaponCtrl) weaponCtrl.enabled = false;

        // 3. SHOW TITLE
        if (bossNameText != null)
        {
            bossNameText.gameObject.SetActive(true);
            bossNameText.text = "THE FINAL BOSS";
        }

        // 4. PAN CAMERA TO BOSS
        float timer = 0f;
        Vector3 bossTarget = new Vector3(boss.transform.position.x, boss.transform.position.y, -10);

        while (timer < 2.0f)
        {
            // Smoothly fly to the boss
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, bossTarget, Time.deltaTime * cameraSpeed);
            timer += Time.deltaTime;
            yield return null;
        }

        // Wait a moment to look at the boss
        yield return new WaitForSeconds(2.0f);

        // 5. PAN BACK TO PLAYER
        Vector3 playerTarget = new Vector3(player.position.x, player.position.y, -10);
        timer = 0f;
        while (timer < 1.0f)
        {
            // Smoothly fly back
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTarget, Time.deltaTime * cameraSpeed * 2);
            timer += Time.deltaTime;
            yield return null;
        }

        // 6. RE-ATTACH CAMERA & RESUME
        if (originalParent != null)
        {
            cameraTransform.parent = originalParent;
            cameraTransform.localPosition = originalLocalPos; // Snap back to exact spot
        }

        if (playerMove) playerMove.enabled = true;
        if (weaponCtrl) weaponCtrl.enabled = true;

        if (bossNameText != null) bossNameText.gameObject.SetActive(false);

        Debug.Log("FIGHT START!");
    }
}