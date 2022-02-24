using UnityEngine;

public class StateIdle : State
{
    public override State Update()
    {
        Controller.playerStats.ChangeStaminaValue(0.1f);
        _direction = HandleInput();
        if ((Controller.attackButtonIsPressed || Input.GetKeyDown(KeyCode.Mouse1) )&& Controller.playerStats.stamina >= 20)
            return ChangeState(new StateAttack(Controller));
        return _direction != Vector2.zero ? ChangeState(new WalkingState(Controller)) : this;
    }

    public StateIdle(PlayerController controller)
    {
        Controller = controller;
        Controller._animator.SetBool("Running", false);
    }

    public override Vector2 HandleInput()
    {
        Vector2 input = new Vector2( Controller.joystick.Horizontal + Input.GetAxisRaw("Horizontal"),Controller.joystick.Vertical + Input.GetAxisRaw("Vertical"));

        return input.normalized;
    }

    public override State ChangeState(State state)
    {
        return state;
    }

    public override Vector2 FixedUpdate()
    {
        return Vector2.zero;
    }
}