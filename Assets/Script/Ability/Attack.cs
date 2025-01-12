
using UnityEngine;
namespace TurnBasedGame {
    
[System.Serializable]
public class Attack
{
    public string attackName;
    public int damage;
    public int manaCost;
    public bool isAOE; // Whether the attack hits multiple targets.
    public GameObject effectPrefab;
    public void Execute(Entity attacker, Entity target)
    {
        if (damage <= attacker.AttackPower)
        {
            damage = attacker.AttackPower;
        }
        if (attacker.Mana >= manaCost)
        {
            attacker.Mana -= manaCost;
            attacker.onManaUse?.Invoke();
            SpawnEffect(attacker, target);
            target.TakeDamage(damage);
            GameManager.Instance.turnManager.NextTurn();
        }
        else
        {
            GameManager.Instance.GenerateLog($"<color=green>{attacker.EntityName}</color> does not have enough mana to use <color=red>{attackName}</color>.");
        }
    }

    public void SpawnEffect(Entity attacker, Entity target)
    {
        if (effectPrefab != null)
        {
            GameManager.Instance.SpawnObject(effectPrefab, target.transform.position, Quaternion.identity);
        }
    }
}
}