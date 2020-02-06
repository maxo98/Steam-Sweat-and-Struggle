using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
	[SerializeField]
	private float speed;
	private float deplHorizontal;
	private float directionLancer = 1;

	[SerializeField]
	private GameObject projectilePrefab;
	private DeplacementProjectile scriptProjectile;
	Rigidbody2D body;

	// Start is called before the first frame update
	void Start()
    {
		body = GetComponent<Rigidbody2D>();

	}

    // Update is called once per frame
    void Update()
    {
		deplHorizontal = Input.GetAxis("Horizontal");
		//Move the character forward
		transform.Translate(Vector3.right.x * Time.deltaTime * speed * deplHorizontal, 0, 0);
		Lancer();
	}

	private void Lancer()
	{
		if (deplHorizontal < -0.01)
			directionLancer = -1;
		if (deplHorizontal > 0.01)
			directionLancer = 1;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			

			GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x + directionLancer * Mathf.Abs(transform.position.x * 0.1f), transform.position.y, 0), projectilePrefab.transform.rotation);
			scriptProjectile = projectile.GetComponent<DeplacementProjectile>();
			scriptProjectile.SetLancer(directionLancer);
		}
	}
}
