using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mover : MonoBehaviour
{
	public abstract void Start();

	public abstract void MoveForward(float moveSpeed);

	public abstract void MoveBackward(float moveSpeed);

	public abstract void RotateRight(float rotateSpeed);

	public abstract void RotateLeft(float roatateSpeed);

	public abstract void Jump();
}
