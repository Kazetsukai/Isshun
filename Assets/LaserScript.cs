using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    float _chargeTime = 0.2f;
    bool _fired = false;

	// Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D rayCastHit = Physics2D.Raycast (transform.position, transform.right);

		if (rayCastHit.collider != null) {
			Debug.Log (rayCastHit.distance);
			//Debug.DrawRay (transform.position, rayCastHit.point - transform.position);
			transform.FindChild("laser_extent").transform.localScale = new Vector3( rayCastHit.distance * 16, 0.5f, 0.5f);
		}
	}

    void FixedUpdate()
    {
        _chargeTime -= Time.fixedDeltaTime;

        if (!_fired && _chargeTime <= 0)
        {
            _fired = true;
            transform.parent = null;
        }

        if (_chargeTime < -2)
        {
				Destroy (gameObject);
        }
    }
}
