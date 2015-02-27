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
