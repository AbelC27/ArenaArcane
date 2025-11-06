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
}