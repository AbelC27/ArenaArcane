using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject auraWeapon;
    public LightningWeapon lightningWeapon;


    public void ShowUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradeMenu.SetActive(true);
    }

    public void HideMenuAndResume()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectUpgrade_Aura()
    {
        if (auraWeapon != null)
        {
            auraWeapon.SetActive(true);
        }
        HideMenuAndResume();
    }

    public void SelectUpgrade_Lightning()
    {
        if (lightningWeapon != null)
        {
            lightningWeapon.enabled = true;
        }
        HideMenuAndResume();
    }

    public void SelectUpgrade_MoveSpeed()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.moveSpeed *= 1.10f;
        }
        HideMenuAndResume();
    }

   
    public void SelectUpgrade_Cooldown()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.cooldownReduction += 0.05f;
        }

        HideMenuAndResume();
    }
}