using UnityEngine;

public class AI_Fast : AIController
{
    public float mulitplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start() //Call the GUARD state by default. If doesPatrol is true, Patrol instead
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        
        if (IsHealthUnder(50))
        {
            pawn.shotsPerSec = baseAttackSpeed * mulitplier;
            pawn.moveSpeed = baseSpeed * mulitplier;
        }
        if (!IsHealthUnder(50))
        {
            pawn.shotsPerSec = baseAttackSpeed;
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
                if (IsTargetOutsideRange(12) && (IsTargetOutsideRange(8)) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                if ((IsTargetInRange(12)) && (CanSee(target)))
                {
                    ChangeState(AIStates.CHASE);
                }
                break;
            case AIStates.CHASE:
                //Action
                Chase();
                Shoot();
                if (IsTargetAbove())
                {
                    Jump();
                }
                //Transition Check
                    if (IsTargetInRange(8))
                    {
                        ChangeState(AIStates.SHOOT);
                    }

                if (IsTargetOutsideRange(12) && doesPatrol == false)
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetOutsideRange(12) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                break;
            case AIStates.SHOOT:
                //Action
                Shoot();
                //Transition Check
                if ((IsTargetInRange(12)) && (IsTargetOutsideRange(5)))
                {
                    ChangeState(AIStates.CHASE);
                }
                if (IsTargetOutsideRange(12) && doesPatrol == false)
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetOutsideRange(12) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                break;
            case AIStates.PATROL:
                //Action
                
                Patrol();
                //Transition Check
                if ((IsTargetInRange(12)) && (IsTargetOutsideRange(8)) && (CanSee(target)))
                {
                    ChangeState(AIStates.CHASE);
                }
                break;
            default:
                //Failsafe do nothing
                break;
        }
    }
}
