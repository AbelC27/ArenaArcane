using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Arcane Arena/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string weaponName = "Basic Spell";

    [Header("Stats")]
    public float damage = 5f;
    public float cooldown = 2f;
    public float projectileSpeed = 10f; // NEW: Speed of the bullet
    public float range = 3f;            // NEW: How long the bullet lasts

    [Header("Multishot Settings")]
    public int projectileCount = 1;     // NEW: Number of bullets per shot
    public float spreadAngle = 15f;     // NEW: Angle between bullets

    public GameObject projectilePrefab;
}