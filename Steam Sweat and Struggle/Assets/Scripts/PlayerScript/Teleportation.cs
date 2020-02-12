using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
	[SerializeField]
	GameObject mapData;
	private float offset = 1f;
	private MapSettings mapSettings;

	// Start is called before the first frame update
	void Start()
    {
		mapSettings = mapData.GetComponent<MapSettings>();
    }

    // Update is called once per frame
    void Update()
    {
       if (transform.position.x > mapSettings.Right)
			transform.position = new Vector3(mapSettings.Left + offset, transform.position.y, 0);
		if (transform.position.x < mapSettings.Left)
			transform.position = new Vector3(mapSettings.Right - offset, transform.position.y, 0);
		if (transform.position.y > mapSettings.Top)
			transform.position = new Vector3(transform.position.x, mapSettings.Bottom + offset, 0);
		if (transform.position.y < mapSettings.Bottom)
			transform.position = new Vector3(transform.position.x, mapSettings.Top - offset, 0);
	}

	
}
