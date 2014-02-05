using System;

public class PowerBattery : ShipComponent
{
	public float Capacity = 100000f;
	public float AvailablePower = 0f;
	public AdvancedShipComponent[] ShipComponents;
	public PowerGenerator[] PowerGenerators;

	void Update ()
	{
		// Do Power generators first.
		foreach (PowerGenerator p in PowerGenerators) {
			p.UpdateInfrastructureConnections();// Move this somewhere to make it check less frequently.
		    // Only operate if the component is alive and has power.
			if (p.healthRef.Alive && p.HasPower)
				continue;
		    // Make sure power generators return a negative value.		
			AvailablePower -= p.Operate(AvailablePower)
		}

		foreach (AdvancedShipComponent c in ShipComponents)
		{
			c.UpdateInfrastructureConnections();// Move this somewhere to make it check less frequently.
		    // Only operate if the component is alive and has power.
			if (c.healthRef.Alive && c.HasPower)
				continue;
		    // Make sure power generators return a negative value.		
			AvailablePower -= c.Operate(AvailablePower)
		}
		
	}
}


