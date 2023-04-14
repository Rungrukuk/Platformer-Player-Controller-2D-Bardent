
using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class LookForPlayerState : State
    {
        private readonly D_LookForPlayerState stateData;

        private bool turnImmediately;
        private bool isAllTurnsDone;
        protected bool isAllTurnsTimeDone;

        private float lastTurnTime;
        private int amountOfTurnsDone;

        protected LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }



        public override void Enter()
        {
            base.Enter();
            if (Movement)
                Movement.SetVelocityX(0);
            amountOfTurnsDone = 0;
            isAllTurnsDone = false;
            isAllTurnsTimeDone = false;
            lastTurnTime = startTime;
        }
        

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Movement)
                Movement.SetVelocityX(0);
            if (turnImmediately)
            {
                Movement.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
                turnImmediately = false;
            }
            else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
            {
                Movement.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
            }

            if (amountOfTurnsDone >= stateData.amountOfTurns)
            {
                isAllTurnsDone = true;
            }

            if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
            {
                isAllTurnsTimeDone = true;
            }
        }



        public void SetTurnImmediately(bool flip)
        {
            turnImmediately = flip;
        }

    }
}
