using UnityEngine;

public class Basic_EngineIncreaseThrust_UI : BasicUIControl {
	public Engine OperatingEngine;
	void OnMouseDown()
	{
		if (Parent.Active) {
			OperatingEngine.ModUsage (0.001f);
		} else {
			// Dont do anything if our console is dead/withoutpower
		}
	}
}