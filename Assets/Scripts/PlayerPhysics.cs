using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask CollisionMask;
	
	private BoxCollider2D _myCollider;

    private List<BoxCollider2D> _levelColliders;

    public Vector2 _velocity;

    public float SpeedDecay;
    public float MaxSpeed;
	
	void Start() {
		_myCollider = GetComponent<BoxCollider2D>();

        _levelColliders = new List<BoxCollider2D>(GameObject.FindObjectsOfType<BoxCollider2D>());
        _levelColliders.Remove(_myCollider);
	}

    public void FixedUpdate()
    {
        // Gravity
        _velocity.y -= 10f * Time.fixedDeltaTime;

        _velocity.x = Mathf.Clamp(_velocity.x, -MaxSpeed, MaxSpeed);
        _velocity.y = Mathf.Clamp(_velocity.y, -MaxSpeed, MaxSpeed);

        transform.position = (Vector2)transform.position + _velocity * Time.fixedDeltaTime;
        transform.position = CollisionResponse(transform.position);
    }

    private Vector2 CollisionResponse(Vector2 position)
    {
        // If we collide with something, push the character outside of it.
        var collider = Physics2D.OverlapArea(_myCollider.center + (Vector2)_myCollider.bounds.min, _myCollider.center + (Vector2)_myCollider.bounds.max, CollisionMask);
        if (collider != null)
        {
            Debug.Log(collider.name);

            var dirs = new Vector2[] {
                new Vector2(collider.bounds.min.x - _myCollider.bounds.max.x, 0), // left
                new Vector2(collider.bounds.max.x - _myCollider.bounds.min.x, 0), // right
                new Vector2(0, collider.bounds.min.y - _myCollider.bounds.max.y), // bottom
                new Vector2(0, collider.bounds.max.y - _myCollider.bounds.min.y) // top
            };

            var dir = dirs.OrderBy(d => d.magnitude).First();

            // Kill any velocity in the direction of pushout
            if (Mathf.Abs(dir.x) > 0.0001f) _velocity.x = 0;
            if (Mathf.Abs(dir.y) > 0.0001f) _velocity.y = 0;

            position += dir;
        }

        return position;
    }
	
	public void Accelerate(Vector2 acceleration) {
        _velocity += acceleration * Time.fixedDeltaTime;
	}

    public void Jump()
    {

    }

    internal void Hold()
    {
        _velocity.x = Mathf.Max(Mathf.Abs(_velocity.x) - (SpeedDecay * Time.fixedDeltaTime), 0) * Mathf.Sign(_velocity.x);
    }
}
