using System.Collections.Generic;
using UnityEngine;
namespace TurnBasedGame
{
    public class Player : Entity
    {
        [SerializeField]
        private Entity target;

        public bool HasTakenAction = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            base.Initialize();
        }

        new void Update()
        {
            base.Update();
        }

        public override void Initialize()
        {
           
        }

        public void AttackTarget()
        {
            Attack(target, 0);
        }



    }
}