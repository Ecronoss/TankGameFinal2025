using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerController : Controller
{
	public float score;
	public bool shotGun;
	[Header("Input Keys")]
	[Header("Player 1")]
	public KeyCode forwardKey;
	public KeyCode backwardKey;
	public KeyCode rightKey;
	public KeyCode leftKey;
	public KeyCode jumpKey;
	public KeyCode shootKey;
	[Header("Player 2")]
	public KeyCode forwardKey2 = KeyCode.UpArrow;
	public KeyCode backwardKey2 = KeyCode.DownArrow;
	public KeyCode rightKey2 = KeyCode.RightArrow;
	public KeyCode leftKey2 = KeyCode.LeftArrow;
	public KeyCode jumpKey2 = KeyCode.RightControl;
	public KeyCode shootKey2 = KeyCode.RightShift;
	[Header("Camera Data")]
	public Camera playerCamera;
	public Vector3 cameraOffset;
	public Vector3 aimOffset;

	public void Awake()
	{
		//Add tank player to GM list
		GameManager.gameInstance.players.Add(this);
		gameObject.name = "Player " + GameManager.gameInstance.players.Count;
		if (gameObject.name == "Player 2")
		{
			forwardKey = forwardKey2;
			backwardKey = backwardKey2;
			rightKey = rightKey2;
			leftKey = leftKey2;
			jumpKey = jumpKey2;
			shootKey = shootKey2;
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public override void Start()
	{
		//Runs start from parent
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		//Run Update() function from parent class
		base.Update();
	}

	public void Possess(Pawn pawnPossess)
	{
		//Pawn to control
		pawn = pawnPossess;
		//Set up camera
		if (playerCamera != null)
		{
			//Move camera to the player + offset
			Vector3 cameraWorldOffset = pawn.transform.TransformDirection(cameraOffset);
			playerCamera.transform.position = pawn.transform.position + cameraWorldOffset;
			//Move camera angle to player + aim offset
			Vector3 palyerWorldAimOffset = pawn.transform.transform.TransformDirection(aimOffset);
			playerCamera.transform.LookAt(pawn.transform.position + palyerWorldAimOffset);
			//Make child of player
			playerCamera.transform.parent = pawn.transform;
			
		}
	}
	
    public void OnDestroy()
	{
		// Remove player from GM list
		GameManager.gameInstance.players.Remove(this);
	}

	public void AddScore(float scoreAdd)
	{
		score += scoreAdd;
	}

	public override void ProcessInputs()
	{
		//This is specific to Player Controller
		if (Input.GetKey(forwardKey) && pawn != null)
		{
			pawn.MoveForward();
		}

		if (Input.GetKey(backwardKey) && pawn != null)
		{
			pawn.MoveBackward();
		}

		if (Input.GetKey(leftKey) && pawn != null)
		{
			pawn.RotateLeft();
		}

		if (Input.GetKey(rightKey) && pawn != null)
		{
			pawn.RotateRight();
		}

		if (Input.GetKeyDown(jumpKey) && pawn != null)
		{
			pawn.Jump();
		}

		if (Input.GetKeyDown(shootKey) && shotGun == false && pawn != null)
		{
			pawn.Shoot();
		}

		if (Input.GetKeyDown(shootKey) && shotGun == true && pawn != null)
		{
			pawn.ShotGun();
		}

		//Do the parent version of function
		base.ProcessInputs();

	}
}
