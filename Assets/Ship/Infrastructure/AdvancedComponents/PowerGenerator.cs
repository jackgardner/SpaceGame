using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Power generator. (Generates Power)
/// </summary>
using System;


public class PowerGenerator : ShipComponent, IResourceComponent {

	// How much base power can this generator produce per tick ?
	public float Capability = 1000f;

	float GetResource(string type, float amount) {
		return 0;
	}
	float PutResource(string type, float amount) {
		return 0;
	}

	public override void Operate ()
	{
		// How many fuel tanks are connected to this generator?
		FuelTank[] ConnectedTanks = (FuelTank[])Inputs.Where (c => c.GetType() == typeof(FuelTank)).ToArray();
		var tanksWithoutFuel = ConnectedTanks.Where(t => t.AvaliableFuel <= 0);
		int OperatingTanks = ConnectedTanks.Count() - tanksWithoutFuel.Count();
	
		ConnectedTanks.Where(x => x.AvaliableFuel > 0)
			.ToList().ForEach(x=> 
			         { 
				x.GetResource("fuel",-(50 / OperatingTanks));
			});

		// Overclocking
		if (Usage > 1) {
			this.health.ModHealth (-50 * Usage);
		}

		// Add Power to all outputs.
		float HealthModifier = this.health.CurrentHealth / this.health.MaxHealth;
		foreach (ShipComponent c in Outputs) {
			c.PutResource("electricity", (Capability * HealthModifier * Usage) / Outputs.Count);
		}
	}
}
