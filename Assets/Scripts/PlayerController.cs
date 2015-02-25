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


        Debug.Log("Horizontal = " + Input.GetAxisRaw("HorizontalAim") + "    Vertical = " + Input.GetAxisRaw("VerticalAim"));
        var sphere = GameObject.Find("Sphere");

        sphere.transform.localPosition = new Vector3(Mathf.RoundToInt(Input.GetAxisRaw("HorizontalAim")), -Mathf.RoundToInt(Input.GetAxisRaw("VerticalAim"))).normalized * 0.5f;

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("JUMP!!!");
            playerPhysics.Jump();
        }

        if (Input.GetButton("Fire2"))
        {

            Debug.DrawLine(sphere.transform.position, sphere.transform.localPosition.normalized, Color.red);
            //playerPhysics.Jump();
        }
	}
}
