using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankPawn : Pawn

{
    public MeshRenderer shield;
    private MeshRenderer shieldMesh;
    private MeshRenderer starMesh;
    public bool arrowOn;
    private Collider starDmg;
    private AudioSource audioSource;
    public AudioClip powerupSFX;
    public PlayerController player1;
    public PlayerController player2;
    public GameManager gameManager;
    public Attributes attributes;
    public GameObject arrow;
    public void Awake()
    {
        Timer timer = GetComponent<Timer>();
        gameManager = GameManager.gameInstance.GetComponent<GameManager>();
        attributes = Attributes.attributesInstance.GetComponent<Attributes>();
    }

    // Start called before first frame update
    protected override void Start()
    {

        //Add tank pawn to GM list
        GameManager.gameInstance.pawns.Add(this);
        gameObject.name = "Tank " + GameManager.gameInstance.pawns.Count;
        //arrow.SetActive(false);

        shieldMesh = transform.Find("Shield").GetComponent<MeshRenderer>();
        if (shieldMesh != null)
        {
            shieldMesh.enabled = false;
        }


        starMesh = transform.Find("MorningStar").GetComponent<MeshRenderer>();
        MeshRenderer[] starMeshChild = transform.Find("MorningStar").GetComponentsInChildren<MeshRenderer>();
        if (starMesh != null)
        {
            starMesh.enabled = false;
            foreach (MeshRenderer renderer in starMeshChild)
            {
                renderer.enabled = false;
            }
        }


        mover = GetComponent<TankMover>();
        shooter = GetComponent<TankShooter>();
        health = GetComponent<Health>();

        //Determine player order
        if (controller != null)
        {
            player1 = GameManager.gameInstance.players[0];
            if (GameManager.gameInstance.isSplitScreen == true)
            {
                player2 = GameManager.gameInstance.players[1];
            }
        }
        GauntCheck();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Powerup Mesh checkers
        ShieldCheck();
        starCheck();
        //Return true if fallen out of the map
        OOBCheck();
        //Real time score tracker
        if (player1 != null)
        {
            score1.text = $"SCORE: {player1.score}";
            if (player2 != null)
            {
                score2.text = $"SCORE: {player2.score}";
            }
        }
        else;
        if (arrow != null)
        {
            if (arrowOn == true)
            {
                arrow.SetActive(true);
            }
            if (arrowOn == false)
            {
                arrow.SetActive(false);
            }
        }
    }

    public void OnDestroy()
    {
        // Remove tank from GM list
        GameManager.gameInstance.pawns.Remove(this);
    }

    private TankMover mover;

    //Forward Backward movement
    public override void MoveForward()
    {
        mover.MoveForward(moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.MoveBackward(moveSpeed);
    }

    //Rotation
    public override void RotateRight()
    {
        mover.RotateRight(rotateSpeed);
    }

    public override void RotateLeft()
    {
        mover.RotateLeft(rotateSpeed);
    }

    //Jumping
    public override void Jump()
    {
        mover.Jump();
    }

    //Shoot
    public override void Shoot()
    {
        shooter.TryShoot(this);
    }

    //Big bullet shot
    public override void ShotGun()
    {
        shooter.TryShotGun(this);
    }

    //Mine Drop
    public override void DropMine()
    {
        shooter.TryDropMine(this);
    }

    public override void RotateToward(Vector3 targetPosition)
    {
        //Find target vector
        Vector3 vectorToTarget = targetPosition - this.transform.position;
        vectorToTarget.y = 0f;
        //Find rotation to aim at vector
        Quaternion rotationVector = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //Change rotation based on turn speed
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationVector, this.rotateSpeed * Time.deltaTime);
    }

    public override void Seek(Vector3 position)
    {
        //Rotate toward target
        RotateToward(position);
        //Movement
        MoveForward();
    }

    //Overrides that refer back to the Seek function
    public override void Seek(GameObject objectTarget)
    {
        Seek(objectTarget.transform.position);
    }

    public override void Seek(Controller controllerTarget)
    {
        Seek(controllerTarget.pawn.gameObject);
    }

    //Activate shield mesh if shield is active
    public override void ShieldCheck()
    {
        if (shieldActive == true)
        {
            if (shieldMesh != null)
            {
                shieldMesh.enabled = true;
            }
        }
        else
        {
            if (shieldMesh != null)
            {
                shieldMesh.enabled = false;
            }
        }
    }

    //Activate Morning Star mesh if mace is active
    public override void starCheck()
    {
        if (starActive == true)
        {
            starOn();
        }
        if (starActive == false)
        {
            starOff();
        }
    }

    public virtual void starOn()
    {
        starActive = true;
        starMesh = transform.Find("MorningStar").GetComponent<MeshRenderer>();
        starDmg = transform.Find("MorningStar").GetComponent<Collider>();
        MeshRenderer[] starMeshChild = transform.Find("MorningStar").GetComponentsInChildren<MeshRenderer>();
        if (starMesh != null)
        {
            starMesh.enabled = true;
            foreach (MeshRenderer renderer in starMeshChild)
            {
                renderer.enabled = true;
                starDmg.enabled = true;
            }
        }
    }
    public virtual void starOff()
    {
        starActive = false;
        starMesh = transform.Find("MorningStar").GetComponent<MeshRenderer>();
        starDmg = transform.Find("MorningStar").GetComponent<Collider>();
        MeshRenderer[] starMeshChild = transform.Find("MorningStar").GetComponentsInChildren<MeshRenderer>();
        if (starMesh != null)
        {
            starMesh.enabled = false;
            foreach (MeshRenderer renderer in starMeshChild)
            {
                renderer.enabled = false;
                starDmg.enabled = false;
            }
        }
    }

    //Plays sfx for collecting powerups and coins
    public void PowerupSound()
    {
        audioSource.PlayOneShot(powerupSFX);
    }

    //Take damage when out of bounds to prevent solftlock
    public void OOBCheck()
    {
        float currentYPos = transform.position.y;
        if (currentYPos < -10f)
        {
            health.TakeTrueDamage(10, this);
        }
    }

    public void GauntCheck()
    {
        if (gameManager.gauntletLevel > 1)
        {
            if (controller != null)
            {
                if (controller.name == "Player 1")
                {
                    shotsPerSec += attributes.P1AttackSpeedAmp;
                    moveSpeed += attributes.P1SpeedAmp;
                    damageDone += attributes.P1DamageAmp;
                    powerupPlus += attributes.P1PowerupAmp;
                    controller.score = attributes.P1Score;
                    if (attributes.P1ArrowRounds > 0)
                    {
                        arrowOn = true;
                    }
                    else;
                    if (attributes.P1ShotGunRounds > 0)
                    {
                        controller.shotGun = true;
                    }
                }
                if (controller.name == "Player 2")
                {
                    shotsPerSec += attributes.P2AttackSpeedAmp;
                    moveSpeed += attributes.P2SpeedAmp;
                    damageDone += attributes.P2DamageAmp;
                    powerupPlus += attributes.P2PowerupAmp;
                    controller.score = attributes.P2Score;
                    if (attributes.P2ArrowRounds > 0)
                    {
                        arrowOn = true;
                    }
                    else;
                    if (attributes.P2ShotGunRounds > 0)
                    {
                        controller.shotGun = true;
                    }
                }
            }
        }
    }
}
