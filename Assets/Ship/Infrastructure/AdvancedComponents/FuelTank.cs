using System;

/// <summary>
/// Fuel tank. (Stores fuel)
/// </summary>
public class FuelTank : ShipComponent
{
	const float CAPACITY = 0f;
	float Avaliable = 0f;
	
	public float PutResourcePrio { get { return CAPACITY / Avaliable; } private set;}
	public float GetResourcePrio { get { return Avaliable / CAPACITY; } private set;}
	
	public override float GetResource(ResourceType type, float amount) {
		if (type != ResourceType.ChemicalFuel)
			return 0;
		
		var amountTaken = Math.Min(Avaliable, amount);
		Avaliable -= amount;
		return amountTaken;
	}
	
	public override float PutResource(ResourceType type, float amount) {
		if (type != ResourceType.ChemicalFuel)
			return amount;
		
		//Let's put all the power in this tank
		Avaliable += amount;
		
		//Did we overfill?
		if (Avaliable > CAPACITY) {
			//Looks like we did. How much by?
			var overfill = Avaliable - CAPACITY;
			
			//Ok so this battery is full
			Avaliable  = CAPACITY;
			
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


