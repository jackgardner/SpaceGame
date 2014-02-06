using UnityEngine;
using System.Collections;


public class NetworkPlayer : Photon.MonoBehaviour {

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	} 
	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp (transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp (transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		}

		// Edit Mode
		if (Input.GetKey(KeyCode.LeftShift)) {
			Screen.lockCursor = false;
			EditMode.On = true;
		} else {
			Screen.lockCursor = true;
			EditMode.On = false;
		}

    	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
