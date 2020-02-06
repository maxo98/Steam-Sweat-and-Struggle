using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
	[SerializeField]
	private Camera camera;
	private float limite = -7;
	private float decalage = 0.3f;

	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		Debug.Log(camera.aspect);
        if (transform.position.x < -camera.orthographicSize/camera.aspect)
			transform.position = new Vector3(camera.orthographicSize*camera.aspect-decalage, transform.position.y, 0);

		if (transform.position.x > camera.orthographicSize/camera.aspect)
			transform.position = new Vector3(-camera.orthographicSize*camera.aspect+decalage, transform.position.y, 0);

		if (transform.position.y < -camera.orthographicSize)
			transform.position = new Vector3(transform.position.x, camera.orthographicSize-decalage, 0);

		if (transform.position.y > camera.orthographicSize)
			transform.position = new Vector3(transform.position.x, -camera.orthographicSize + decalage, 0);
	}
}
