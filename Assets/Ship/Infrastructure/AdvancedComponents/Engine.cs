﻿using UnityEngine;
using System.Collections;

public class Engine : AdvancedShipComponent {

	public float Strength = 10f;
	public Vector3 Direction { get { return this.transform.forward; } }
	public GameObject Hull;
	
	public override float Operate(float availablePower)
	{
		float projectedPowerUsage = (Strength * Usage) * 650f;
		
                if ((availablePower - projectedPowerUsage) > 0){
                        Hull.rigidbody.AddForceAtPosition(Direction * Strength * Usage, transform.position);
                        return projectedPowerUsage;
                }
                else {
                        // Just dont function if we havent got the power, possibly add some sort of inefficent movement here.
                        return 0;
                }
	}
}