using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIStates { GUARD, CHASE, RUN, BACKUP, SHOOT, JUMP, PATROL }
    public AIStates currentState;

    public float selections;
    public PlayerController[] allTanks;
    protected float lastStateChangeTime;
    protected bool hasTarget;
    public Transform selfLocation;
    public bool hasKey;

    [Header("Patrol")]
    //ADD LIST FOR WAYPOINTS
    public List<Transform> waypoints;
    private int currentWaypoint = 0;
    public float seekDistance = 1;
    public bool doesPatrol;
    private float lookAheadDistance = 2;

    [Header("Detection")]
    public float hearingSense;
    public float fieldOfView;

    [Header("Stat Overrides for FSM")]
    //Stats that can change mid-game
    //Variable for any speed changes

    public float baseSpeed;
    public float baseAttackSpeed;
    public float baseRotateSpeed;
    [Header("Targeting")]
    public GameObject P1Obj;
    public GameObject P2Obj;
    public PlayerController P1Control;
    public PlayerController P2Control;
    protected GameObject target;
    private float targetTime;
    private Pawn pawn1;
    private Pawn pawn2;
    private GameObject pawnObj1;
    private GameObject pawnObj2;
    private float P1Dist;
    private float P2Dist;

    public void Awake()
    {
        baseSpeed = pawn.moveSpeed;
        baseAttackSpeed = pawn.shotsPerSec;
        baseRotateSpeed = pawn.rotateSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        ChangeState(AIStates.GUARD);
        //Add tank AI to GM list
        GameManager.gameInstance.AI.Add(this);
        gameObject.name = "AI " + GameManager.gameInstance.AI.Count;
        GetPlayerData();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (targetTime < Time.time)
        {
            targetTime = Time.time + 0.6f;
            TargetNearestPlayer();
        }
        ProcessInputs();
    }

    protected bool HasTarget()
    {
        return (target != null);
    }

    public void TargetPlayerByNumber(int number)
    {
        if (GameManager.gameInstance != null)
        {
            if (GameManager.gameInstance.players.Count > 0)
            {
                if (GameManager.gameInstance.players[number] != null)
                {
                    target = GameManager.gameInstance.players[number].pawn.gameObject;
                    return;
                }
            }
        }

        Debug.LogWarning("ERROR: PLAYER " + number + " Does not exist");
    }

    public void Patrol()
    {
        //Go to current waypoint
        pawn.Seek(waypoints[currentWaypoint].position);
        //Go to next waypoint after reaching first
        if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= seekDistance)
        {
            //Track next waypoint
            currentWaypoint++;
            //Loop back to start
            if (currentWaypoint >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
    }
    public void Guard()
    {
        //Look around.
        pawn.RotateLeft();
        //Check if we can hear target
        if (target == null)
        {
            foreach (Controller player in GameManager.gameInstance.players)
            {
                if (CanHear(player.pawn.gameObject))
                {
                    target = player.pawn.gameObject;
                    return;
                }
                if (CanSee(player.pawn.gameObject))
                {
                    target = player.pawn.gameObject;
                    return;
                }
            }
        }
        //Target P0 if null
    }

    public void Chase()
    {
        //Turn toward player. Move forward
        pawn.RotateToward(target.transform.position);
        pawn.MoveForward();
    }

    public void Run()
    {
        //Turn away from player. move forward
        //Create reverse vector
        Vector3 vectorToTarget = -1 * (target.transform.position - pawn.transform.position);
        pawn.RotateToward(pawn.transform.position + vectorToTarget);
        pawn.MoveForward();
    }

    public void Backup()
    {
        //Turn toward player. Move backward
        pawn.RotateToward(target.transform.position);
        pawn.MoveBackward();
    }

    public void Shoot()
    {
        //Stand still. Shoot
        pawn.RotateToward(target.transform.position);
        pawn.Shoot();
    }

    public void ShotGun()
    {
        //Stand still. Shoot
        pawn.RotateToward(target.transform.position);
        pawn.ShotGun();
    }

    public void DropMine()
    {
        //Stand still. Drop Mine
        pawn.DropMine();
    }

    public void Jump()
    {
        //Jump when obstacle is blocking
        pawn.Jump();
    }

    public override void ProcessInputs()
    {
        //Switch Case based on enum
        Guard();

    }

    public bool CanSee(GameObject target)
    {
        if (target == null) //No target
        {
            return false;
        }

        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        float angleToTarget = Vector3.Angle(vectorToTarget, pawn.transform.forward);
        //If greater than view angle, object is out of sight
        if (angleToTarget > fieldOfView)
        {
            return false;
        }
        //Viewpoint
        Vector3 eyeLevel = pawn.transform.position + (Vector3.up * 1);
        //Cast ray
        RaycastHit hitInfo;
        if (Physics.Raycast(eyeLevel, vectorToTarget, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.gameObject == target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public bool CanHear(GameObject target)
    {
        if (target == null) //No target
        {
            return false;
        }

        float distanceToTarget = Vector3.Distance(target.transform.position, pawn.transform.position);
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        if (targetNoiseMaker == null) //If it can't make noise, ignore
        {
            return false;
        }
        //If hearing and noisemaker overlap
        if (targetNoiseMaker.noiseVolume + hearingSense > distanceToTarget)
        {
            return true;
        }
        return false;
    }

    public bool IsHealthUnder(float hpPercent)
    {
        float currentHpPercent = (pawn.health.currentHP / pawn.health.maxHP) * 100;
        if (currentHpPercent <= hpPercent) //Check Current HP to see if it is under threshold
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTargetOutsideRange(float distance)
    {
        if (target == null)
        {
            return false;
        }

        if (Vector3.Distance(pawn.transform.position, target.transform.position) > distance) //Check distance from target
        {
            return true;
            target = null;
        }
        else
        {
            return false;
        }
    }

    public bool IsTargetInRange(float distance)
    {
        if (target == null)
        {
            return false;
        }

        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance) //Check distance from target
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTargetAbove()
    {
        if (pawn.transform.position.y < (target.transform.position.y - 1)) //Check if player is above
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsBlocked() //Jump over obstacles
    {
        Ray ray = new Ray(); //cast ray
        ray.origin = pawn.transform.position; //set ray position
        ray.direction = pawn.transform.forward; //set ray direction
        RaycastHit hitData = new RaycastHit(); //save ray data
        if (Physics.Raycast(ray, out hitData, lookAheadDistance)) //check ray collision
        {
            if (hitData.collider.gameObject != target)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeState(AIStates newState)
    {
        //Change state
        currentState = newState;
        //Time switched
        lastStateChangeTime = Time.time;
    }

    protected void TargetNearestPlayer()
    {
        //If tank kills player and leaves null target, target next player
        if (target == null)
        {
            TargetPlayerByNumber(0);
        }

        //Get distance between self and player 1
        if (P1Control.pawn != null)
        {
            P1Dist = Vector3.Distance(pawn.transform.position, P1Control.pawn.transform.position);
        }
        //Get distance between self and player 2 only if multiplayer is active
        if (GameManager.gameInstance.isSplitScreen == true && P2Control.pawn != null && P1Control.pawn != null)
        {
            P2Dist = Vector3.Distance(pawn.transform.position, P2Control.pawn.transform.position);
            //Target player based on which is closer
            if (P1Dist >= P2Dist)
            {
                TargetPlayerByNumber(1);
            }
            else
            {
                TargetPlayerByNumber(0);
            }
        }
    }

    protected void GetPlayerData()
    {
        //Get components needed to calculate distance of players for targeting
        P1Obj = GameObject.Find("Player 1");
        P1Control = P1Obj.GetComponent<PlayerController>();
        if (GameManager.gameInstance.isSplitScreen == true)
        {
            P2Obj = GameObject.Find("Player 2");
            P2Control = P2Obj.GetComponent<PlayerController>();

        }
    }
}
