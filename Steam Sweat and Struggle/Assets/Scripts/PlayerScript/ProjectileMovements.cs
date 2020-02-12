using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovements : MonoBehaviour
{
	[SerializeField]
	private float speed = 2500;

	[SerializeField]
	private float direction = 1;

	private Rigidbody2D body;
	private Collider2D collider2d;

	// Start is called before the first frame update
	void Start()
    {
		body = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
		body.AddForce(transform.right * speed * direction);
	}

    // Update is called once per frame
    void Update()
    {
		
		//transform.Translate(direction * Time.deltaTime * speed * body.velocity.x, body.velocity.y, 0);
	}

	public void setThrowDirection(float throwDirection)
	{
		direction = throwDirection;
	}

	private void OnTriggerEnter(Collider other)
	{
		body.velocity = new Vector2(0, 0);
	}
	
}

