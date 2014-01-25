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
	public List<ChunkObject> MapObjects = new List<ChunkObject>();

	const float MAP_SIZE = 1000f;
	IWorldStreamSource StreamSource;
	// Use this for initialization
	void Start ()
	{
		StreamSource = new InMemoryWorldStream ();
		MapObjects = StreamSource.GetObjects (new Bounds (Ship.transform.position, Vector3.one * MAP_SIZE));
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
		Bounds generateDistance = new Bounds (Ship.transform.position, Vector3.one * 250f);
		// I.E We've moved somewhere.
		if (Ship.transform.position != oldShipPosition) {
			foreach (ChunkObject co in MapObjects.Where(o => generateDistance.Intersects(new Bounds(o.Position,Vector3.one)) && !LiveObjects.Exists(g => g.transform.position == o.Position))){
				GameObject newobj = PhotonNetwork.InstantiateSceneObject(co.ChunkObjectType, co.Position, co.Rotation,0,null);
				LiveObjects.Add(newobj);
				newobj.transform.parent = this.transform;
			}
			foreach (GameObject go in LiveObjects.Where(o => !generateDistance.Intersects(new Bounds(o.transform.position,new Vector3(3,3,3))))){
				DeleteCO(go);
			}
		}
		oldShipPosition = Ship.transform.position;
	}

	void DeleteCO(GameObject go)
	{
		Debug.Log ("Deleting" + go.transform.position.ToString ());
		PhotonNetwork.Destroy (go);
	}
}
