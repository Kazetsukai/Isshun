using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask CollisionMask;
	
	private BoxCollider2D _myCollider;

    private List<BoxCollider2D> _levelColliders;

    private Vector2 _velocity;
    private float _jumpTime = 0;
    private bool _jumping = false;

    public float Gravity;
    public float SpeedDecay;
    public float MaxSpeedHorizontal;
    public float MaxSpeedVertical;
    public float MaxJumpTime;
    public float JumpSpeed;
	
	void Start() {
		_myCollider = GetComponent<BoxCollider2D>();

        _levelColliders = new List<BoxCollider2D>(GameObject.FindObjectsOfType<BoxCollider2D>());
        _levelColliders.Remove(_myCollider);
	}

    public void FixedUpdate()
    {
        // Gravity
        _velocity.y -= Gravity * Time.fixedDeltaTime;

        if (_jumping && _jumpTime > 0)
        {
            _velocity.y = JumpSpeed;
            _jumpTime -= Time.fixedDeltaTime;
        }
        else
        {
            _jumpTime = 0;
        }

        _velocity.x = Mathf.Clamp(_velocity.x, -MaxSpeedHorizontal, MaxSpeedHorizontal);
        _velocity.y = Mathf.Clamp(_velocity.y, -MaxSpeedVertical, MaxSpeedVertical);

        transform.position = (Vector2)transform.position + _velocity * Time.fixedDeltaTime;

		CollisionResponse(transform.position);

		GetComponent<Animator> ().SetBool ("moving", _velocity.magnitude > 0.01f);

        _jumping = false;
    }

    private void CollisionResponse(Vector2 position)
    {
        int iterationsLeft = 4;

        while (iterationsLeft > 0)
        {
            iterationsLeft--;

            // If we collide with something, push the character outside of it.
            var colliders = Physics2D.OverlapAreaAll(_myCollider.offset + (Vector2)_myCollider.bounds.min, _myCollider.offset + (Vector2)_myCollider.bounds.max, CollisionMask);

            foreach (var collider in colliders)
            {
                Debug.Log(collider.name);

                var dirs = new Vector2[] {
                    new Vector2(Mathf.Min(collider.bounds.min.x - _myCollider.bounds.max.x, 0), 0), // left
                    new Vector2(Mathf.Max(collider.bounds.max.x - _myCollider.bounds.min.x, 0), 0), // right
                    new Vector2(0, Mathf.Min(collider.bounds.min.y - _myCollider.bounds.max.y, 0)), // bottom
                    new Vector2(0, Mathf.Max(collider.bounds.max.y - _myCollider.bounds.min.y, 0)) // top
                };

                var dir = dirs.OrderBy(d => d.magnitude).First();

                // Kill any velocity in the direction of pushout
                if (Mathf.Abs(dir.x) > 0.0001f) 
                {
                    _velocity.x = 0;

                    transform.position += (Vector3)dir;
                    iterationsLeft = 0;
                }
                if (Mathf.Abs(dir.y) > 0.0001f)
                {
                    // Reset jump if we hit a floor and are not trying to jump
                    if (!_jumping && dir.y > 0)
                        _jumpTime = MaxJumpTime;
                    else
                        _jumpTime = 0; // Stop a jump if we hit a roof

                    _velocity.y = 0;

                    transform.position += (Vector3)dir;
                    iterationsLeft = 0;
                }
            }
           
            if (colliders.Length == 0)
            {
                iterationsLeft = 0;
            }
        }
    }
	
	public void Accelerate(Vector2 acceleration) {
        _velocity += acceleration * Time.fixedDeltaTime;
	}


    public void Jump()
    {
        _jumping = true;
    }

    internal void Hold()
    {
        _velocity.x = Mathf.Max(Mathf.Abs(_velocity.x) - (SpeedDecay * Time.fixedDeltaTime), 0) * Mathf.Sign(_velocity.x);
    }
}
