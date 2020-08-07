using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAction : MonoBehaviour
{
    
    private float timeBeforeActivation = 3.0f;
    
    private float activatedTime;

    private bool activated;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        activatedTime = Time.time+timeBeforeActivation;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.time>activatedTime && !activated) {
            Sprite mineAct = (Sprite) Resources.Load("Graphic/ProjectileGraphics/MineActivated", typeof(Sprite));
            spriteRenderer.sprite = mineAct;
            activated = true;
        }
    }
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Characters" && activated)
		{
			//Character dies
			other.gameObject.SendMessage("OnDie");
			Destroy(gameObject);
		}
	}
}
