using UnityEngine;
using System.Collections.Generic;
using System;
namespace TurnBasedGame
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        private EntityStats entityStats;
        private string entityName;
        public string EntityName { get => entityName; set => entityName = value; }
        private int maxHealth;
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        private int health;
        public int Health { get => health; set => health = value; }
        private int attackPower;
        public int AttackPower { get => attackPower; set => attackPower = value; }
        private int defense;
        public int Defense { get => defense; set => defense = value; }
        private int mana = 0;
        public int Mana { get => mana; set => mana = value; }
        private int maxMana = 0;
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public bool isDefend;
        [SerializeField] private int defendManaCost = 15;
        public int DefendManaCost { get => defendManaCost; set => defendManaCost = value; }
        public GameObject defendEffectPrefab;
        private GameObject defendEffectActive;
        public Action onInitialized;
        public Action onBuffChange;
        public Action onTakeDamage;
        public Action onManaUse;

        [Header("Effects")]
        [SerializeField] private GameObject diedEffectPrefab;

        public List<Attack> attacks = new List<Attack>();
        [SerializeField]
        private AvailableBuff availableBuffs = new AvailableBuff();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Initialize();
            InitializeBuffs();
        }

        // Update is called once per frame
        public void Update()
        {

        }

        // Initialize the entity with default values
        public virtual void Initialize()
        {
            entityName = entityStats.entityName;
            health = entityStats.health;
            maxHealth = entityStats.health;
            attackPower = entityStats.attackPower;
            defense = entityStats.defense;
            mana = entityStats.mana;
            maxMana = entityStats.mana;
            onInitialized?.Invoke();
            
        }

        // Initialize the available buffs for the entity
        private void InitializeBuffs()
        {
        }

        public void UseBuff(int buffIndex)
        {
            switch (buffIndex)
            {
                case 0:
                    ApplyBuff(availableBuffs.attackBuffs[0]);
                    GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> uses <color=red>{availableBuffs.attackBuffs[0].BuffName}</color> buff. Attack power increased by {availableBuffs.attackBuffs[0].BuffAmount} for {availableBuffs.attackBuffs[0].Duration} turns.");
                    break;
                case 1:
                    ApplyBuff(availableBuffs.defenseBuffs[0]);
                    GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> uses <color=red>{availableBuffs.defenseBuffs[0].BuffName}</color> buff. Defense increased by {availableBuffs.defenseBuffs[0].BuffAmount} for {availableBuffs.defenseBuffs[0].Duration} turns.");
                    break;
                case 2:
                    ApplyBuff(availableBuffs.manaBuffs[0]);
                    GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> uses <color=red>{availableBuffs.manaBuffs[0].BuffName}</color> buff. Mana increased by {availableBuffs.manaBuffs[0].BuffAmount} for {availableBuffs.manaBuffs[0].Duration} turns.");
                    break;
            }
            GameManager.Instance.turnManager.NextTurn();
        }

        public void ApplyBuff(IBuff buff)
        {
            buff.Apply(this);
            GameManager.Instance.turnManager.RegisterBuff(this, buff);
            onBuffChange?.Invoke();
        }

        public void RemoveBuff(IBuff buff)
        {
            buff.Remove(this);
            onBuffChange?.Invoke();
        }
        // Method to take damage
        public void TakeDamage(int damage)
        {
            int damageTaken;
            DamageFlash();
            if (isDefend)
            {
                damageTaken = Mathf.Max(0, damage - Defense);
            }
            else
            {
                damageTaken = damage;
            }
            GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> takes <color=red>{damageTaken}</color> damage.");
            health -= damageTaken;
            if (health <= 0)
            {
                Die();
            }
            isDefend = false;
            CheckDefendState();
            onTakeDamage?.Invoke();
        }

        public void Attack(Entity target, int Attackindex)
        {
            attacks[Attackindex].Execute(this, target);
        }

        public void Defend()
        {
            if (mana < defendManaCost)
            {
                GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> does not have enough mana to defend.");
                return;
            }
            if (!isDefend)
            {
                isDefend = true;
                GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> defends.");
                defendEffectActive = Instantiate(defendEffectPrefab, transform.position, Quaternion.identity);
            }
            mana -= defendManaCost;
            onManaUse?.Invoke();
            GameManager.Instance.turnManager.NextTurn();

        }

        void CheckDefendState()
        {
            Debug.Log("Checking defend state");
            if (isDefend == false && defendEffectActive != null)
            {
                Destroy(defendEffectActive);
            }
            else
            {
                Debug.Log("Defend not active");
            }
        }

        public virtual void Die()
        {
            GameManager.Instance.GenerateLog($"<color=green>{entityName}</color> has died.");
            GameManager.Instance.SpawnObject(diedEffectPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.GameOver(this);
            Destroy(this.gameObject, 1f);
        }

        Color32 originalColor;
        public void DamageFlash()
        {
            // Damage flash animation
            originalColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.red;
            Invoke(nameof(ResetColor), 0.1f);
        }

        private void ResetColor()
        {
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }
    public enum BuffType
    {
        Attack,
        Defense,
        Mana
    }
    [System.Serializable]
    public struct AvailableBuff
    {
        public List<AttackBuff> attackBuffs;
        public List<DefenseBuff> defenseBuffs;
        public List<ManaBuff> manaBuffs;
    }
}

