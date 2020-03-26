using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spec1 : PlayerController
{
	public override void Move() {
		if (dashing>0) {
			--dashing;
		} else {
		    //moving the character on the X and Y axis
		    float fSpd = (movements.x < 0) ? fastFallSpeed : fallSpeed;
		    body.velocity = new Vector2(body.velocity.x * 3 / 4 + (movements.x * speed * 1.5f) * 1 / 4, Mathf.Max(body.velocity.y, fSpd));
		}
	}
}
