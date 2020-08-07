﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAction : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private float directionAngle;

	private Rigidbody2D body;
	private Collider2D collider2d;

	// Start is called before the first frame update
	void Start()
    {
		body = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();

		//we use cos(angle) and sin(angle) to normalize speed in every direction
		body.AddForce(new Vector2(transform.right.x * speed * Mathf.Cos(directionAngle), transform.up.y * speed * Mathf.Sin(directionAngle)), ForceMode2D.Impulse);
		transform.Rotate(0,0, (180 / Mathf.PI) * directionAngle);
	}

    // Update is called once per frame
    void Update()
    {
		
		
	}

	public void SetDirectionAngle(float direction)
	{
		directionAngle = direction;
	}
	public void SetSpeed(float spd)
	{
		speed = spd;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "platforms" || other.gameObject.tag == "Characters")
		{
			if (other.gameObject.tag == "Characters") {
				//Character dies
				other.gameObject.SendMessage("OnDie");
				Destroy(gameObject);
			} else {
				/*var delta = Mathf.PI;
				//we use cos(angle) and sin(angle) to normalize speed in every direction
				Vector2 velocity = body.velocity;
				velocity = new Vector2(-velocity.x, -velocity.y);
				body.velocity = velocity;
				transform.Rotate(0,0, (180 / Mathf.PI) * delta);
				directionAngle+=delta;*/
			}
		}
	}
	
}

