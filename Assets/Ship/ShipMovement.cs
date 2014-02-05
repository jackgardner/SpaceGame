using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// class to handle ship movement
/// </summary>
public class ShipMovement: MonoBehaviour {
	public GameObject ShipNetwork;

	// Update is called once per frame
    	void Update ()
	{	
		ShipNetwork.transform.position = this.transform.position;
		ShipNetwork.transform.rotation = this.transform.localRotation;
	}
}
