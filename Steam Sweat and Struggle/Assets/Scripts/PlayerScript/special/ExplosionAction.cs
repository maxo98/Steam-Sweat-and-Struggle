using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAction : MonoBehaviour
{
    private GameObject parent;
    private float delta = 0.4f;
    private float scale = 0.1f;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        gameObject.transform.localScale = new Vector3(CalculScaleFunc(scale), CalculScaleFunc(scale), 1.0f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float CalculScaleFunc(float v) {
        return 5*Mathf.Sqrt(v);
    }
    void Update()
    {
        scale += delta;
        gameObject.transform.localScale = new Vector3(CalculScaleFunc(scale), CalculScaleFunc(scale), 1.0f);
        if (scale>11) {
            float alpha = (15-scale)/4;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
        if (scale>15){
            Destroy(gameObject);
        }
    }
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Characters" && other.gameObject!=parent)
		{
			//Character dies
			other.gameObject.SendMessage("OnDie");
		}
	}

    private void SetParent(object p) {
        parent = (GameObject) p;
    }
}
