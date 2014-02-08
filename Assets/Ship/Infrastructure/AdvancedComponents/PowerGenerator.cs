using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Power generator. (Generates Power)
/// </summary>
using System;


public class PowerGenerator : ShipComponent {

	// How much base power can this generator produce per tick ?
	const float MAX_FUEL_USAGE = 50f;
	const float POWER_PER_UNIT_FUEL = 20f;


	public float PutResourcePrio { get { return 0; } private set;}
	public float GetResourcePrio { get { return 0; } private set;}

	public override float GetResource(ResourceType type, float amount) {
		return 0;
	}
	public override float PutResource(ResourceType type, float amount) {
		return amount;
	}
	public void Update()
	{
		// Call me elsewhere
		this.Operate();
	}

	public override void Operate (InfrastructureNode infrastructure)
	{
		var f = infrastructure.GetResource(ResourceType.ChemicalFuel, MAX_FUEL_USAGE);
		infrastructure.PutResource(ResourceType.Power, f * POWER_PER_UNIT_FUEL);
	}
}
