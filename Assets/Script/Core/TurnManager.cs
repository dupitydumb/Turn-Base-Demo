using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace TurnBasedGame
{
    public class TurnManager : MonoBehaviour
    {
        public Action onTurnChanged;
        [SerializeField]
        public TurnState turnState;
        [SerializeField]
        private Player player;
        [SerializeField]
        private Enemy enemy;

        [SerializeField]
        private GameObject playerActionPanel;

        private List<(Entity entity, IBuff buff)> activeBuffs = new List<(Entity, IBuff)>();
        private int currentTurn;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            onTurnChanged += CheckState;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void NextTurn()
        {
            

            if (turnState == TurnState.PlayerTurn)
            {
                turnState = TurnState.EnemyTurn;
                onTurnChanged?.Invoke();
                Debug.LogWarning("Enemy Turn");
            }
            else
            {
                currentTurn++;
                RemoveBuff();
                turnState = TurnState.PlayerTurn;
                onTurnChanged?.Invoke();
                Debug.LogWarning("Player Turn");
                
            }

        }

        public void RegisterBuff(Entity entity, IBuff buff)
        {
            activeBuffs.Add((entity, buff));
        }

        public void RemoveBuff()
        {
            List<(Entity entity, IBuff buff)> buffsToRemove = new List<(Entity, IBuff)>();
            foreach (var buff in activeBuffs)
            {
                if (buff.buff.Duration == currentTurn + 1)
                {
                    buff.entity.RemoveBuff(buff.buff);
                    buffsToRemove.Add(buff);
                }
            }
            foreach (var buff in buffsToRemove)
            {
                activeBuffs.Remove(buff);
                GameManager.Instance.GenerateLog($"<color=green>{buff.entity.EntityName}</color> has lost <color=red>{buff.buff.BuffName}</color> buff.");
            }
        }

        void CheckState()
        {
            if (turnState == TurnState.PlayerTurn)
            {
                playerActionPanel.SetActive(true);
            }
            else
            {
                playerActionPanel.SetActive(false);
                enemy.TakeAction();
            }
        }
    }
    

    public enum TurnState
    {
        PlayerTurn,
        EnemyTurn
    }
}