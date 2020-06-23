using UnityEngine;
public enum Tile { Stone, Dirt, Air, Gravel, Lava, Water };
[CreateAssetMenu(fileName = "Damageable Tile", menuName = "Damageable Tile")]
public class DamagableTile : ScriptableObject
{
    public GameObject prefab;
    public int baseDamage;
    public Tile tileType;
    public bool hasGravity;
}