using System;

/// <summary>
/// Power battery. (Stores power)
/// </summary>
public class PowerBattery : ShipComponent
{
	float Capacity = 0f;
	float Avaliable = 0f;

	float GetResource(string type, float amount) {
		if (type != "electricity")
			return 0;
		
		var amountTaken = Math.Min(Avaliable, amount);
		Avaliable -= amount;
		return amountTaken;
	}

	float PutResource(string type, float amount) {
		if (type != "electricity")
			return amount;
		
		//Let's put all the power in this battery
		Avaliable += amount;
		
		//Did we overfill?
		if (Avaliable > Capacity) {
			//Looks like we did. How much by?
			var overfill = Avaliable - Capacity;
			
			//Ok so this battery is full
			Avaliable  = Capacity;
			
			//And you still have this much power left to put somewhere else
			return overfill;
		}
		
		//No overfilling here, yay
		return 0;
	}

	public override void Operate ()
	{
		throw new NotImplementedException ();
	}
}

