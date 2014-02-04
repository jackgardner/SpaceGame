using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// class to handle ship movement
/// </summary>
public class ShipMovement: MonoBehaviour {

	public PowerGenerator[] PowerGenerators;
	public ShipStation[] ShipStations;
	public AdvancedShipStation[] AdvancedShipStations;
	public Engine[] Engines;

    public ShipComponent[] ShipComponents;

    public GameObject ShipInterior;
	public GameObject ShipNetwork;

    float AvaliablePower = 0f;

	// 1 Unit of thrust = 0.5 Power Units
	public float AvaliablePower = 0f;

	void Start ()
	{
		// Just a git test
	}
	
	// Update is called once per frame
    	void Update ()
	{
		foreach (ShipComponent c in ShipComponents)
		{
			if (c.healthRef.Alive)
				continue;
		    // Make sure power generators return a negative value.		
			AvailablePower -= s.Operate(AvailablePower)
		}
		
		ShipNetwork.transform.position = this.transform.position;
		ShipNetwork.transform.rotation = this.transform.localRotation;
	}

	void OnGUI()
	{	int stuff = (int)Math.Round(AvaliablePower); // Round to friendly number
		GUILayout.Label (stuff.ToString());
	}


}
