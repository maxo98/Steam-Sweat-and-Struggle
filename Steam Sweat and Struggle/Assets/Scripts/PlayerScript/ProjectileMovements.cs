using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovements : MonoBehaviour
{
	[SerializeField]
	private float parentSpeedFactor = 1.0f;

	[SerializeField]
	private float speed;

	[SerializeField]
	private float directionAngle;

	private Rigidbody2D body;
	private Collider2D collider2d;

	private MapSettings settings;

	// Start is called before the first frame update
	void Start()
    {
		settings = GetComponent<Teleportation>().GetMapData().GetComponent<MapSettings>();
		body = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();

		//we use cos(angle) and sin(angle) to normalize speed in every direction
		body.AddForce(new Vector2(transform.right.x * speed * Mathf.Cos(directionAngle), transform.up.y * speed * Mathf.Sin(directionAngle)), ForceMode2D.Impulse);
		transform.Rotate(0,0, (180 / Mathf.PI) * directionAngle);
	}

    // Update is called once per frame
    void Update()
    {
		
		if (parentSpeedFactor!=settings.GetShotSpeed()) {
			float newSpeed = settings.GetShotSpeed();
			Vector2 velocity = body.velocity;
			body.velocity = new Vector2(velocity.x*newSpeed/parentSpeedFactor, velocity.y*newSpeed/parentSpeedFactor);
			body.gravityScale = body.gravityScale*newSpeed/parentSpeedFactor;
			parentSpeedFactor = newSpeed;
		}
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
			}
			Destroy(gameObject);
		}
	}
	
}

