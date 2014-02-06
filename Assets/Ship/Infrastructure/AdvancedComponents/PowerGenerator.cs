using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerGenerator : ShipComponent {

	// How much power can this generator produce per tick ?
	public float Capability = 1000f;

	public override void Operate ()
	{
		// How many fuel tanks are connected to this generator?
		FuelTank[] ConnectedTanks = Inputs.Where (c => c.GetType == typeof(FuelTank));
		var tanksWithoutFuel = ConnectedTanks.Where(t => t.AvaliableFuel <= 0);
		int OperatingTanks = ConnectedTanks.Count - tanksWithoutFuel.Count;
		
		ConnectedTanks.Where(x => x.AvaliableFuel > 0)
			.ToList().ForEach(x=> 
			         { 
				x.ModFuel(-(Usage * 50 / OperatingTanks));
			});

		// Overclocking
		if (Usage > 1) {
			this.health.ModHealth (-50 * Usage);
		}

		// Add Power
		float HealthModifier = this.health.CurrentHealth / this.health.MaxHealth;
		foreach (ShipComponent c in Outputs) {
			c.ModPower((Capability * HealthModifier * Usage) / Outputs.Count);
		}
	}
}
