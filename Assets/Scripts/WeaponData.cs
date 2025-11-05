using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Arcane Arena/Weapon Data")]
public class WeaponData : ScriptableObject
{

    [Header("Info")]
    public string weaponName = "Basic Spell";

    [Header("Stats")]
    public float damage = 5f;
    public float cooldown = 2f;

    public GameObject projectilePrefab;
}