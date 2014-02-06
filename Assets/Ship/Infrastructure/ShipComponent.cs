﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour , IResourceComponent
{
	public List<IResourceComponent> Inputs;
	public List<IResourceComponent> Outputs;

	public Health health;
	
	public float Usage = 0;
	public float MaxUsage = 0;

	public float GetResource (string type, float amount)
	{
		throw new UnityException ("How did you get here?");
	}
	public float PutResource (string type, float amount)
	{
		throw new UnityException ("How did you get here?");
	}

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
