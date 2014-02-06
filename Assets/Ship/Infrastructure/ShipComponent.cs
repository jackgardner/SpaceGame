using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour
{
	public List<ShipComponent> Inputs;
	public List<ShipComponent> Outputs;

	public Health health;
	
	public float Usage = 0;
	public float MaxUsage = 0;

	public float AvaliablePower = 0;
	public float PowerCapacity = 0;

	public float FuelCapacity = 0;
	public float AvaliableFuel = 0;

	public void ModHealth (float amount)
	{
		health.ModHealth(amount);
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

	public void ModPower (float amount)
	{
		AvaliablePower += amount;

		if (AvaliablePower < 0) {
			AvaliablePower = 0;
		}
		if (AvaliablePower > PowerCapacity) {
			AvaliablePower = PowerCapacity;
		}
	}
	public void ModFuel (float amount)
	{
		AvaliableFuel += amount;

		if (AvaliableFuel < 0) {
			AvaliableFuel = 0;
		}
		if (AvaliableFuel > FuelCapacity) {
			AvaliableFuel = FuelCapacity;
		}
	}

	void OnMouseDown ()
	{
		if (EditMode.On) {
			if (EditMode.From != null)
				EditMode.From = this;
			else{
				EditMode.Too = this;
				//Dynamic Attach logic.

				this.Inputs.Add(EditMode.From);

				EditMode.From = null;
				EditMode.Too = null;
			}
				
		}
	}


	/// <summary>
        /// Called once per frame, this is an opportunity for this component to use power
        /// </summary>
        /// <param name="availablePower">The total amount of power available to this component</param>
        /// <returns>The amount of power this component consumed</returns>
	public abstract void Operate();
}
