using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	// Use this for initialization

    public GameObject playerToMimic;
    public GameObject playerPhysicalShip;

    void Start () {
        this.transform.parent = playerPhysicalShip.transform;
        this.transform.position = Vector3.zero;

	}

    void Update()
    {
		this.transform.localPosition = playerToMimic.transform.localPosition;
		this.transform.localRotation = playerToMimic.transform.localRotation;
    }

}


