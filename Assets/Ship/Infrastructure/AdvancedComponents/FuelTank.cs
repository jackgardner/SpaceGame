using System;

/// <summary>
/// Fuel tank. (Stores fuel)
/// </summary>
public class FuelTank : ShipComponent
{
	public override void Operate ()
	{
		// Find all attached components with less than max fuel capacity, then siphon more in.
		Inputs.ForEach(c => {
			if (c.AvaliablePower < c.FuelCapacity)
			{
				c.ModFuel(c.FuelCapacity - c.AvaliableFuel);
				this.ModFuel(c.AvaliableFuel - c.FuelCapacity);
			}
		});
	}
}


