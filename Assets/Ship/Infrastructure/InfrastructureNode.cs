//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

public class InfrastructureNode
{
	List<IResourceComponent> IResourceComponents;

	public float GetResource(ResourceType type, float amount)
	{
		float accumulator = 0;
		foreach (var component in IResourceComponents
		         .OrderByDescending(c => c.GetResourcePrio)) {
			// .ORDERBY GET PRIORITY
			accumulator += component.GetResource(type, amount);
			
			if (accumulator >= amount)
				break;
		}

		return accumulator;
	}

	public float PutResource(ResourceType type, float amount)
	{
		float accumulator = amount;
		foreach (var component in IResourceComponents
		         .OrderByDescending(c => c.PutResourcePrio)) {
			// .ORDERBY GET PRIORITY
			accumulator = component.PutResource(type, amount);
			
			if (accumulator = 0)
				break;
		}
		
		return accumulator;
	}
}


