using UnityEngine;
using System.Collections;

public class PowerGenerator : ShipComponent {
	public float Capability = 10000f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	}
	
	public override float Operate(float availablePower)
	{
	        float Output = 0f;
	        float HealthModifier = this.health.CurrentHealth / this.health.MaxHealth;
	        
	        // Overclocking
	        if (Usage > 1){
	            this.health.ModHealth(-50 * Usage);
	        }
	        
	        Output = (Capability * HealthModifier * Usage);
	        // Return negative because its generating power.
			return -Output;
	}
}
