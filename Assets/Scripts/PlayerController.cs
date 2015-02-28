using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	//Player handling
	public float Acceleration = 8;

	PlayerPhysics playerPhysics;

    public GameObject LaserObject;

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
            FireLaser(sphere.transform.position, sphere.transform.position + sphere.transform.localPosition.normalized * 100);
        }
	}

    void FireLaser(Vector3 start, Vector3 end)
    {
        if (GameObject.Find("Laser(Clone)") == null)
        {
            var newLaser = (GameObject)Instantiate(LaserObject, start, Quaternion.FromToRotation(Vector3.right, end - start));
            newLaser.transform.parent = transform;
            newLaser.transform.FindChild("laser_extent").transform.localScale = new Vector3(100, 0.5f, 0.5f);
        }
    }
}
