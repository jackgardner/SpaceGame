using UnityEngine;
using System.Collections;

/// <summary>
/// Ion engine. (Powered by electricity)
/// </summary>
public class IonEngine : ShipComponent {

	public float Strength = 10f;
	public Vector3 Direction { get { return this.transform.forward; } }
	public GameObject Hull;
	
	public override void Operate()
	{
		float projectedPowerUsage = (Strength * Usage) * 650f;
		
                if ((AvaliablePower - projectedPowerUsage) > 0){
                        Hull.rigidbody.AddForceAtPosition(Direction * Strength * Usage, transform.position);

			// Overclocking
			if (Usage > 1) {
				this.health.ModHealth (-50 * Usage);
			}
                }
                else {
                        // Just dont function if we havent got the power, possibly add some sort of inefficent movement here.
                }
	}
}
