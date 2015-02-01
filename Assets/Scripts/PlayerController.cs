using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	//Player handling
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;

	public float jumpHeight = 12;

	float currentSpeed;
	float targetSpeed;
	Vector2 amountToMove;

	PlayerPhysics playerPhysics;

	void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
	}

	void Update () 
	{	
		// Reset acceleration upon collision
		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}

		if (playerPhysics.grounded)
		{
			amountToMove.y = 0;

			//Jump
			if (Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;
			}
		}
		//Input
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

		//Set amount to move
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);
	}

	float IncrementTowards(float n, float target, float a)
	{
		if (n == target)
		{
			return n;
		}
		else
		{
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target - n))? n: target;
		}
	}
}
