using UnityEngine;
using System.Collections;

/// <summary>
/// Ion engine. (Powered by electricity)
/// </summary>
public class IonEngine : ShipComponent {

	public float Strength = 10f;
	public Vector3 Direction { get { return this.transform.forward; } }
	public GameObject Hull;

	const float MAX_POWER_USAGE = 150f;
	const float THRUST_PER_UNIT_POWER = 0.05f;

	public override float GetResource(ResourceType type, float amount) {
		return 0;
	}
	public override float PutResource(ResourceType type, float amount) {
		return amount;
	}

	public override void Operate(InfrastructureNode infrastructure)
	{
		// Overclocking
		if (Usage > 1) {
			this.health.ModHealth (-50 * Usage);
		}

		var f = infrastructure.GetResource(ResourceType.Electricity, MAX_POWER_USAGE * Usage);
		Hull.rigidbody.AddForceAtPosition(Direction * f * THRUST_PER_UNIT_POWER , transform.position);
	}
}
