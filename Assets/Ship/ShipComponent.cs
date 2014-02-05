using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour
{
	public Health health;
	public float Usage;
	public float MaxUsage;

	// Use this for initialization
	void Start () {
		health = this.GetComponent<Health>();;
	}

	public void ModHealth (float amount)
	{
		health.ModHealth(amount);
	}
	
	// Update is called once per frame
	void Update ()
	{
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
