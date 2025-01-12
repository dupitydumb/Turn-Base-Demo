using UnityEngine;
using System.Collections;
using NUnit.Framework;
namespace TurnBasedGame
{
    public class Enemy : Entity
    {
        [SerializeField]
        private Entity target;

        private bool HasTakenAction = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            base.Initialize();
            if (GameManager.Instance.turnManager != null)
            {
                GameManager.Instance.turnManager.onTurnChanged += CheckTurnState;
            }
            else
            {
                Debug.LogError("TurnManager is not initialized.");
            }
        }

        new void Update()
        {
            base.Update();
        }

        public override void Initialize()
        {
            
        }

        public void TakeAction()
        {
            if (HasTakenAction)
            {
                return;
            }
            else
            {
                StartCoroutine(DelayedAction());
            }
        }

        private IEnumerator DelayedAction()
        {
            Debug.Log("Delayed Action");

            // Wait for 2 seconds before taking action
            yield return new WaitForSeconds(2.0f);
            // randomly select an action
            int randomAction = Random.Range(0, 2);
            if (randomAction == 0 && Mana > attacks[0].manaCost)
            {
                Attack(target, 0);
                Debug.Log("Attack");
            }
            else if (randomAction == 0 && Mana <= attacks[0].manaCost)
            {
                UseBuff(2);
                Debug.Log("Use Mana Buff");
            }
            else if (randomAction == 1 && isDefend == false && Mana > DefendManaCost)
            {
                Defend();
                Debug.Log("Defend");
            }
            else
            {
                //Apply random buff
                int randomBuff = Random.Range(0, 2);
                switch (randomBuff)
                {
                    case 0:
                        UseBuff(0);
                        Debug.Log("Enemy Use Attack Buff");
                        break;
                    case 1:
                        UseBuff(1);
                        Debug.Log("Enemy Use Defense Buff");
                        break;
                }
            }

            HasTakenAction = false;
        }

        public void CheckTurnState()
        {
            if (GameManager.Instance.turnManager.turnState == TurnState.EnemyTurn)
            {
                TakeAction();
                HasTakenAction = true;
            }
        }
    }
}