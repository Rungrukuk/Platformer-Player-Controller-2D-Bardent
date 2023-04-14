using _Scripts.Player.PlayerStates.SuperStates;


public class P_AttackState : P_AbilityState
{
    public P_AttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
