using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class DeadState : State
    {
        private readonly D_DeadState stateData;

        protected DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_DeadState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            var position = entity.transform.position;
            Object.Instantiate(stateData.deathChunkParticles,position,stateData.deathChunkParticles.transform.rotation);
            Object.Instantiate(stateData.deathBloodParticles, position, stateData.deathBloodParticles.transform.rotation);
            entity.gameObject.SetActive(false);
        }
    
    }
}
