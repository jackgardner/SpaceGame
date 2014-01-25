using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class WorldGenerator : Photon.MonoBehaviour
{
	public int Seed;
	public GameObject Ship;
	public Vector3 oldShipPosition;

	public List<GameObject> LiveObjects = new List<GameObject>();

	const float CHUNK_SIZE = 1000f;
	IWorldStreamSource StreamSource;
	// Use this for initialization
	void Start ()
	{
		StreamSource = new BasicWorldStream ();
		Bounds generateDistance = new Bounds (Ship.transform.position, Vector3.one * CHUNK_SIZE);
		foreach (ChunkObject c in StreamSource.GetObjects(generateDistance)){
			LiveObjects.Add(PhotonNetwork.InstantiateSceneObject(c.ChunkObjectType, c.Position, c.Rotation,0,null));
		}
	}
	
	// Sync the map seed.
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        // So I don't really know how photon works, but you *really* need to make sure the seed is synced before generating a single thing.
        // I'd have a boolean field like:
        //
        // bool isSeedAvailable = NETWORKING.IsThisTheServer;
        //
        // And then in this method (when a non server receives the seed) it sets that to true. Obviously then in all your generate methods you check of the flag is set.
        // And if it isn't refuse to generate a thing
    
		if (stream.isWriting) // What are we sending to the server?
		{
			stream.SendNext(Seed);
		}
		else // What should we be recieving from the server?
		{
			Seed = (int)stream.ReceiveNext();
			UnityEngine.Random.seed = Seed;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Bounds generateDistance = new Bounds (Ship.transform.position, Vector3.one * CHUNK_SIZE);
		// I.E We've moved somewhere.
		if (Ship.transform.position != oldShipPosition) {
			foreach (GameObject go in LiveObjects.Where(o => !generateDistance.Intersects(new Bounds(o.transform.position,Vector3.one)))){
				DeleteCO(go);
			}
			foreach (ChunkObject c in StreamSource.GetObjects(generateDistance)){
				GameObject newobj = PhotonNetwork.InstantiateSceneObject(c.ChunkObjectType, c.Position, c.Rotation,0,null);
				LiveObjects.Add(newobj);
			}
		}
		oldShipPosition = Ship.transform.position;
	}

	void DeleteCO(GameObject go)
	{
		PhotonNetwork.Destroy (go);
		LiveObjects.Remove (go);
	}
}
