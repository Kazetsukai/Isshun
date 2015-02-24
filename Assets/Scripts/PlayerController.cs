using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	//Player handling
	public float Acceleration = 8;

	PlayerPhysics playerPhysics;

	void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
	}

	void FixedUpdate() 
	{
		//Input
        var accel = Input.GetAxisRaw("Horizontal") * Acceleration;

        if (Mathf.Abs(accel) < 0.01f)
            playerPhysics.Hold();
        else
            playerPhysics.Accelerate(new Vector2(accel, 0));


        if (Input.GetButton("Fire1"))
        {
            Debug.Log("JUMP!!!");
            playerPhysics.Jump();
        }
	}
}
