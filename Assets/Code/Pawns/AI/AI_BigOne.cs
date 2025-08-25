using UnityEngine;

public class AI_BigOne : AIController
{
    public float mulitplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start() //Call the GUARD state by default. If doesPatrol is true, Patrol instead
    {
        base.Start();
        if (doesPatrol == true)
        {
            ChangeState(AIStates.PATROL);
        }
    }

    public override void Update()
    {
        base.Update();
        if (IsHealthUnder(50))
        {
            pawn.moveSpeed = baseSpeed + mulitplier;
        }
        if (!IsHealthUnder(50))
        {
            pawn.moveSpeed = baseSpeed;
        }
        if (IsBlocked())
            {
                Jump();
            }
    }
    public override void ProcessInputs()
    {
        //TODO: Switch Case based on enum
        switch (currentState)
        {
            case AIStates.GUARD:
                //Action
                
                Guard();
                //Transition Check
                if ((IsTargetInRange(15)) && (IsHealthUnder(50)))
                {
                    ChangeState(AIStates.CHASE);
                }
                if ((IsTargetInRange(15)) && (!IsHealthUnder(50)))
                {
                    ChangeState(AIStates.SHOOT);
                }
                break;
            case AIStates.CHASE:
                //Action
                pawn.moveSpeed = baseSpeed;
                Chase();
                ShotGun();
                DropMine();
                if (IsTargetAbove())
                {
                    Jump();
                }
                //Transition Check
                if (IsTargetOutsideRange(20))
                {
                    ChangeState(AIStates.GUARD);
                }
                break;
            case AIStates.SHOOT:
                //Action
                ShotGun();
                //Transition Check
                if ((IsTargetInRange(15)) && (IsHealthUnder(50)))
                {
                    ChangeState(AIStates.CHASE);
                }
                if (IsTargetOutsideRange(20))
                {
                    ChangeState(AIStates.GUARD);
                }
                break;
            default:
                //Failsafe do nothing
                break;
        }
    }
}
