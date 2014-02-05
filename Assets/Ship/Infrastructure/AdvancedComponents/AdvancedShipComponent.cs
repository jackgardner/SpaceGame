using System;
using System.Collections.Generic;
using System.Linq;

public class AdvancedShipComponent : ShipComponent
{
	public float Usage;
	public float MaxUsage;
	public List<List<InfraComponent>> PowerConduits;
	public bool HasPower = false;

	public void UpdateInfrastructureConnections ()
	{
		foreach (List<InfraComponent> connection in PowerConduits) {
			if (connection.All(ic => ic.health.Alive))
				HasPower = true;
		}
		HasPower = false;
	}

	public void ModUsage(float amount)
	{
		Usage += amount;
		if (Usage > MaxUsage)
			Usage = MaxUsage;
		if ((Usage <= 0)) {
			Usage = 0;
		}
	}

	/// <summary>
        /// Called once per frame, this is an opportunity for this component to use power
        /// </summary>
        /// <param name="availablePower">The total amount of power available to this component</param>
        /// <returns>The amount of power this component consumed</returns>
	public abstract float Operate(float availablePower);
}


