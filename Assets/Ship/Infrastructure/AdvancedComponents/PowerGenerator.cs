using UnityEngine;
using System.Collections;

public class PowerGenerator : AdvancedShipComponent {

	// How much power can this generator produce per tick ?
	public float Capability = 1000f;

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
