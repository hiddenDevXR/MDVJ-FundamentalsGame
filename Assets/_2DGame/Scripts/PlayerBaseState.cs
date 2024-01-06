using UnityEngine;

public abstract class PlayerBaseState
{
    protected int index = 0;

    public abstract void EnterState(PlayerController player);
    public abstract void UpdateState(PlayerController player);
    public abstract void FixedUpdateState(PlayerController player);
}

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState()
    {
        index = 0;
    }

    public override void EnterState(PlayerController player)
    {
    }

    public override void UpdateState(PlayerController player)
    {
    }

    public override void FixedUpdateState(PlayerController player)
    {
    }
}

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
    }

    public override void UpdateState(PlayerController player)
    {
    }

    public override void FixedUpdateState(PlayerController player)
    {
    }
}


public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
    }

    public override void UpdateState(PlayerController player)
    {
    }

    public override void FixedUpdateState(PlayerController player)
    {
    }
}

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState()
    {
        index = 2;
    }

    public override void EnterState(PlayerController player)
    {
        player.m_animator.SetInteger("State", index);
    }

    public override void UpdateState(PlayerController player)
    {
    }

    public override void FixedUpdateState(PlayerController player)
    {
    }
}
