using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
	private float limite = -7;

	private float bordGauche = -10.96f;
	private float bordDroit = 10.96f;
	private float bordBas = -5.17f;
	private float bordHaut = 5.17f;
	private float decalage = 0.3f;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < bordGauche)
			transform.position = new Vector3(bordDroit-decalage, transform.position.y, 0);

		if (transform.position.x > bordDroit)
			transform.position = new Vector3(bordGauche+decalage, transform.position.y, 0);

		if (transform.position.y < bordBas && transform.position.y > limite)
			transform.position = new Vector3(transform.position.x, bordHaut-decalage, 0);

		if (transform.position.y > bordHaut)
			transform.position = new Vector3(transform.position.x, bordBas + decalage, 0);
	}
}
