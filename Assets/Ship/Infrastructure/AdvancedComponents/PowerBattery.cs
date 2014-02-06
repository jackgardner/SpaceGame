using System;

public class PowerBattery : ShipComponent
{
	public override void Operate ()
	{
		// Find all attached components with less than max power capacity, then siphon more in.
		Inputs.ForEach(c => {
			if (c.AvaliablePower < c.PowerCapacity)
			{
				c.ModPower(c.PowerCapacity - c.AvaliablePower);
				this.ModPower(c.AvaliablePower - c.PowerCapacity);
			}
		});
	}
}

