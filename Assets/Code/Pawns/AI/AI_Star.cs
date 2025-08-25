using UnityEngine;

public class AI_Star : AIController
{
    public float mulitplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start() //Call the GUARD state by default. If doesPatrol is true, Patrol instead
    {
        base.Start();
    }

    public override void Update()
    {
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
        Pawn targetPawnComponent = GetComponent<Pawn>();
        if (targetPawnComponent != null)
        {
            //Activate Morning Star
            targetPawnComponent.starActive = true;
        }
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
                if (IsTargetOutsideRange(10) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                if ((IsTargetInRange(10)))
                {
                    ChangeState(AIStates.CHASE);
                }
                break;
            case AIStates.PATROL:
                //Action
                
                Patrol();
                //Transition Check
                if ((IsTargetInRange(10)))
                {
                    ChangeState(AIStates.CHASE);
                }
                break;
            case AIStates.CHASE:
                //Action
                Chase();
                DropMine();
                //Transition Check
                if (IsTargetOutsideRange(10) && doesPatrol == true)
                {
                    ChangeState(AIStates.PATROL);
                }
                if (IsTargetOutsideRange(10) && doesPatrol == false)
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
