using UnityEngine;

namespace TurnBasedGame {

    [System.Serializable]
    public class AttackBuff : IBuff
    {
        [SerializeField] private string buffName;
        public string BuffName => buffName;
        [SerializeField] private int buffAmount;
        public int BuffAmount => buffAmount;
        private int originalValue;
        [SerializeField] private int duration;

        public int Duration => duration;

        public AttackBuff(int buffAmount, int duration, string buffName)
        {
            this.buffAmount = buffAmount;
            this.duration = duration;
            this.buffName = buffName;
        }

        public void Apply(Entity entity)
        {
            originalValue = entity.AttackPower;
            entity.AttackPower += buffAmount;
        }

        public void Remove(Entity entity)
        {
            entity.AttackPower = originalValue;
        }
    }
    [System.Serializable]
    public class DefenseBuff : IBuff
    {
        [SerializeField] private string buffName;
        public string BuffName => buffName;
        [SerializeField] private int buffAmount;
        public int BuffAmount => buffAmount;
        private int originalValue;
        [SerializeField] private int duration;

        public int Duration => duration;

        public DefenseBuff(int buffAmount, int duration, string buffName)
        {
            this.buffAmount = buffAmount;
            this.duration = duration;
            this.buffName = buffName;
        }

        public void Apply(Entity entity)
        {
            originalValue = entity.Defense;
            entity.Defense += buffAmount;
        }

        public void Remove(Entity entity)
        {
            entity.Defense = originalValue;
        }
    }
    [System.Serializable]
    public class ManaBuff : IBuff
    {
        [SerializeField] private string buffName;
        public string BuffName => buffName;
        [SerializeField] private int buffAmount;
        public int BuffAmount => buffAmount;
        private int originalValue;
        [SerializeField] private int duration;

        public int Duration => duration;

        public ManaBuff(int buffAmount , int duration, string buffName)
        {
            this.buffAmount = buffAmount;
            this.duration = duration;
            this.buffName = buffName;
        }

        public void Apply(Entity entity)
        {
            originalValue = entity.Mana;
            entity.Mana += buffAmount;
        }

        public void Remove(Entity entity)
        {
            entity.Mana = originalValue;
        }
    }

    public interface IBuff
    {
        public string BuffName { get; }
        public int Duration { get; }
        void Apply(Entity entity);
        void Remove(Entity entity);
    }


}