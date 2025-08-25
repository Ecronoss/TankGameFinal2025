using UnityEngine;

public class AI_Scared : AIController
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
                pawn.moveSpeed = baseSpeed * mulitplier;
                pawn.rotateSpeed = 360;
            }
        if (!IsHealthUnder(50))
        {
            pawn.moveSpeed = baseSpeed;
            pawn.rotateSpeed = baseRotateSpeed;
        }
        if (IsBlocked())
            {
                Jump();
            }
    }
    public override void ProcessInputs()
    {
        //Switch Case based on enum
        switch (currentState)
        {
            case AIStates.GUARD:
                
                //Action
                Guard();
                //Transition Check
                if (IsTargetOutsideRange(10) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                if ((IsTargetInRange(10)) && (!IsHealthUnder(50)))
                {
                    ChangeState(AIStates.BACKUP);
                }
                if ((IsTargetInRange(10)) && (IsHealthUnder(50)))
                {
                    ChangeState(AIStates.RUN);
                }
                break;
            case AIStates.RUN:
                //Action
                Run();
                DropMine();
                //Transition Check
                if (IsTargetOutsideRange(10) && doesPatrol == false)
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetOutsideRange(10) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                break;
            case AIStates.BACKUP:
                //Action
                Backup();
                Shoot();
                //Transition Check
                if (IsTargetOutsideRange(10) && doesPatrol == false)
                {
                    ChangeState(AIStates.GUARD);
                }
                if (IsTargetOutsideRange(10) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                if ((IsTargetInRange(10)) && (IsHealthUnder(50)))
                {
                    ChangeState(AIStates.RUN);
                }
                break;
            case AIStates.PATROL:
                //Action
                
                Patrol();
                //Transition Check
                if ((IsTargetInRange(10)) && (!IsHealthUnder(50)))
                {
                    ChangeState(AIStates.BACKUP);
                }
                if ((IsTargetInRange(10)) && (IsHealthUnder(50)))
                {
                    ChangeState(AIStates.RUN);
                }
                break;
            default:
                //Failsafe do nothing
                break;
        }
    }
}
