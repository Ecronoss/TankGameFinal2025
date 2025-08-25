using UnityEngine;

public class AI_Gunner : AIController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start() //Call the GUARD state by default. If doesPatrol is true, Patrol instead
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        
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
                if (((IsTargetInRange(15)) && (IsTargetOutsideRange(10)) || (CanHear(target))))
                {
                    ChangeState(AIStates.SHOOT);
                }
                break;
            case AIStates.CHASE:
                //Action
                pawn.moveSpeed = baseSpeed;
                Chase();
                Shoot();
                //Transition Check
                if (IsTargetInRange(10))
                {
                    ChangeState(AIStates.SHOOT);
                }

                if (IsTargetOutsideRange(30))
                {
                    ChangeState(AIStates.GUARD);
                }
                break;
            case AIStates.BACKUP:
                //Action
                pawn.moveSpeed = (baseSpeed * 2);
                Backup();
                //Transition Check
                if ((IsTargetInRange(20)) && (IsTargetOutsideRange(10)))
                {
                    ChangeState(AIStates.CHASE);
                }
                if (IsTargetOutsideRange(30))
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetInRange(25))
                {
                    ChangeState(AIStates.SHOOT);
                }
                break;
            case AIStates.SHOOT:
                //Action
                Shoot();
                //Transition Check
                if ((IsTargetInRange(20)) && (IsTargetOutsideRange(10)))
                {
                    ChangeState(AIStates.CHASE);
                }
                if (IsTargetOutsideRange(30))
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetInRange(15))
                {
                    ChangeState(AIStates.BACKUP);
                }
                break;
            default:
                //Failsafe do nothing
                break;
        }
    }
}
