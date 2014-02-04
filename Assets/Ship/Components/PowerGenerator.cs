using UnityEngine;
using System.Collections;

public class PowerGenerator : ShipComponent {
	public float Usage = 1f;
	public float Capability = 10000f;
	// Use this for initialization
	void Start () {
		
	}

	public override void ModUsage (float amount)
	{
		Usage += amount;
		if (Usage > 1.5f)
			Usage = 1.5;
		if ((Usage <= 0)) {
			Usage = 0;
		}
	}

	// Update is called once per frame
	void Update () {
	}
	
	public override float Operate(float availablePower)
	{
        float Output = 0f;
        float HealthModifier = this.health.CurrentHealth / this.health.MaxHealth; // need a proper way to handle HPModifers here
        
        // Overclocking
        if (Usage > 1){
            this.health.ModHealth(-50 * Usage);
        }
        
        Output = (Capability * Modifier * Usage);
        // Return negative because its generating power.
		return -Output;
	}
}
