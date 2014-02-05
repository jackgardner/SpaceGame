using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour
{
	public Health health;

	public void ModHealth (float amount)
	{
		health.ModHealth(amount);
	}


}
