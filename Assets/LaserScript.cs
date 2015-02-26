using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    float _chargeTime = 0.2f;
    bool _fired = false;

    public Renderer Renderer;

	// Use this for initialization
    void Start()
    {
        var col = Renderer.material.color;
        Renderer.material.color = new Color(col.r, col.g, col.b, 0.1f);
	
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


            var col = Renderer.material.color;
            Renderer.material.color = new Color(col.r, col.g, col.b, 1);
        }

        if (_fired)
        {
            var col = Renderer.material.color;

            if (col.a <= 0)
                Destroy(gameObject);

            Renderer.material.color = new Color(col.r, col.g, col.b, col.a - Time.fixedDeltaTime);
        }
    }
}
