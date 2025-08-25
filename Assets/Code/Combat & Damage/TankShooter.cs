using UnityEngine;

public class TankShooter : Shooter
{
    public GameObject projectilePrefab;
    public GameObject minePrefab;
    public Transform shootPosition;
    public Transform minePosition;
    public float shootTime;
    public float mineTime;
    public float shootForce;
    public float shotVolume;
    private AudioSource audiosource;
    public AudioClip shootSound;

    private NoiseMaker noiseMaker;
    private Pawn pawn;

    public override void Start()
    {
        noiseMaker = GetComponent<NoiseMaker>();
        pawn = GetComponent<Pawn>();
        audiosource = GetComponent<AudioSource>();
    }

    public override void TryShoot(Pawn shooterPawn)
    {
        if (Time.time > shootTime)
        {
            Shoot(shooterPawn);
        }
    }

    public override void TryShotGun(Pawn shooterPawn)
    {
        if (Time.time > shootTime)
        {
            ShotGun(shooterPawn);
        }
    }

    public override void TryDropMine(Pawn shooterPawn)
    {
        if (Time.time > mineTime)
        {
            DropMine(shooterPawn);
        }
    }

    public override void Shoot(Pawn shooterPawn)
    {
        //Create bullet at position, rotation, and scale of the shoot pos
        GameObject bulletObject = Instantiate<GameObject>(projectilePrefab, shootPosition.position, shootPosition.rotation);

        //Get DamageOnHit component from bullet
        DamageOnHit damageComponent = bulletObject.GetComponent<DamageOnHit>();
        damageComponent.damageDealt = shooterPawn.damageDone;
        damageComponent.damageDealer = shooterPawn;

        //Get the bullet rigidbody
        Rigidbody bulletRB = bulletObject.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletObject.transform.forward * shooterPawn.shootForce);

        //Sets shoot delay
        shootTime = Time.time + (1 / shooterPawn.shotsPerSec);

        //Make some noise
        if (noiseMaker != null)
        {
            noiseMaker.MakeNoise(shotVolume);
        }

        audiosource.PlayOneShot(shootSound);
    }

    public override void ShotGun(Pawn shooterPawn)
    {
        //Create bullet at position, rotation, and scale of the shoot pos
        GameObject bulletObject = Instantiate<GameObject>(projectilePrefab, shootPosition.position, shootPosition.rotation);
        bulletObject.transform.localScale = new Vector3(1f, 1f, 1f);
        //Get DamageOnHit component from bullet
        DamageOnHit damageComponent = bulletObject.GetComponent<DamageOnHit>();
        damageComponent.damageDealt = shooterPawn.damageDone;
        damageComponent.damageDealer = shooterPawn;
        //Give bullet special properties
        damageComponent.multiHit = true;

        //Get the bullet rigidbody
        Rigidbody bulletRB = bulletObject.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletObject.transform.forward * shooterPawn.shootForce);

        //Sets shoot delay
        shootTime = Time.time + (1 / shooterPawn.shotsPerSec);
        //Make some noise
        if (noiseMaker != null)
        {
            noiseMaker.MakeNoise(shotVolume+10);
        }
        audiosource.PlayOneShot(shootSound);
    }
    
    public override void DropMine(Pawn shooterPawn)
    {
        bool grounded = pawn.isGrounded;
        if (grounded == true)
        {
            //Create bullet at position, rotation, and scale of the shoot pos
            GameObject mineObject = Instantiate<GameObject>(minePrefab, minePosition.position, minePosition.rotation);
            mineObject.transform.localScale = new Vector3(1f, 1f, 1f);
            //Get DamageOnHit component from bullet
            DamageOnHit damageComponent = mineObject.GetComponent<DamageOnHit>();
            damageComponent.damageDealt = shooterPawn.mineDamage;

            //Sets shoot delay
            mineTime = Time.time + (1 / shooterPawn.dropsPerSec);
        }
    }
}
