using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats", order = 1)]
public class EntityStats : ScriptableObject
{
    public string entityName;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defense;
    public int mana;
    public int maxMana;

}
