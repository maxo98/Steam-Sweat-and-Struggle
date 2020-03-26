using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAction : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeActivation = 3.0f;
    [SerializeField]
    private float activatedTime;

    void Start()
    {
        activatedTime = Time.time+timeBeforeActivation;
    }

    void Update()
    {
        
    }
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Characters" && Time.time>activatedTime)
		{
			//Character dies
			other.gameObject.SendMessage("OnDie");
			Destroy(gameObject);
		}
	}
}
