using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public bool infinite;
    public float damageDealt;
    public Pawn damageDealer;
    public float lifeSpan = 2;
    public bool multiHit = false;
    public float multiHitCooldown = 0;
    private float nextHit;
    public AudioSource audioSource;
    public AudioClip hitSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextHit = Time.time - 1;
        if (infinite == false)
        {
            Destroy(gameObject, lifeSpan);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
       public void OnTriggerEnter(Collider other)
    {
        //Play Sound
        
        if (audioSource != null && !audioSource.isPlaying)
        {
            //audioSource.PlayOneShot(hitSound);
        }

        //Deal Damage if has HP
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth != null && nextHit <= Time.time)
        {
            otherHealth.TakeDamage(damageDealt, other.GetComponent<Pawn>(), damageDealer);
            nextHit = Time.time + multiHitCooldown;
        }
        //Destroys bullet on impact
        if (multiHit == false)
        {
            Destroy(gameObject);
        }
    }
}
