using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class PlatformBehaviour : MonoBehaviour {

	BoxCollider2D collider;

	// Use this for initialization
	void Start () 
	{
		collider = GetComponent<BoxCollider2D>();	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Get the direction and distance that the collision occured in
		Vector2 colDistance = other.gameObject.transform.position - this.transform.position;

		//Move the object in the direction it came from until it is no longer inside the platform bounds
		//while (other.collider.transform.position


		//Debug.Log (colDistance);


	}
}
