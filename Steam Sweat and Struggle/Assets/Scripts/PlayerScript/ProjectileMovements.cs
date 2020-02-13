using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovements : MonoBehaviour
{
	[SerializeField]
	private float speed = 500;

	[SerializeField]
	private float direction = 1;

	private Rigidbody2D body;
	private Collider2D collider2d;

	// Start is called before the first frame update
	void Start()
    {
		body = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
		body.AddForce(transform.right * speed * direction, ForceMode2D.Impulse);
	}

    // Update is called once per frame
    void Update()
    {
		
		
	}

	public void setThrowDirection(float throwDirection)
	{
		direction = throwDirection;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "platforms" || other.gameObject.tag == "Characters")
		{
			Destroy(gameObject);
		}
	}
	
}

