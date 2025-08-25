using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public abstract class Pawn : MonoBehaviour
{
	public PlayerController controller;
	public float moveSpeed;
	public float rotateSpeed;
	public float jumpHeight;
	public bool isGrounded;
	public float damageDone = 1;
	public float mineDamage = 2;
	public float shootForce = 500;
	public float shotsPerSec = 2;
	public float dropsPerSec = 1;
	public float powerupPlus = 0;
	public bool shieldActive;
	public bool starActive;
	public TextMeshProUGUI score1;
	public TextMeshProUGUI score2;
	public Health healthComp;

	[HideInInspector] public Shooter shooter;
	[HideInInspector] public Health health;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected abstract void Start();

	// Update is called once per frame
	protected abstract void Update();

	public abstract void MoveForward();

	public abstract void MoveBackward();

	public abstract void RotateRight();

	public abstract void RotateLeft();

	public abstract void Jump();

	public abstract void Shoot();

	public abstract void ShotGun();
	public abstract void DropMine();

	public abstract void RotateToward(Vector3 position);

	public abstract void Seek(Vector3 position);

	public abstract void Seek(GameObject objectTarget);

	public abstract void Seek(Controller controllerTarget);

	public virtual void OnCollisionStay()
	{

		if (GetComponent<Rigidbody>().linearVelocity.y <= 0) // Ensures that the tank is grounded for jump check
		{
			Ray ray = new Ray(); //cast ray
			ray.origin = this.transform.position; //set ray position
			ray.direction = this.transform.up * -1; //set ray direction
			RaycastHit hitData = new RaycastHit(); //save ray data
			if (Physics.Raycast(ray, out hitData, 1)) //check ray collision
			{
				if (hitData.collider.gameObject)
				{
					isGrounded = true;
				}
			}
		}

	}

	public abstract void ShieldCheck();
	public abstract void starCheck();
	
}
