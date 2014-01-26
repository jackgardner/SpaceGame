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

	public int mapobjcount;
	const float MAP_SIZE = 25000f;
	const float VIEW_DISTANCE = 3000f;
	IWorldStreamSource StreamSource;
	// Use this for initialization
	void Start ()
	{
		StreamSource = new InMemoryWorldStream (Seed);
		MapObjects = StreamSource.GetObjects (new Bounds (Ship.transform.position, Vector3.one * MAP_SIZE));
		mapobjcount = MapObjects.Count;
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
		// I.E We've moved somewhere.
		if (Ship.transform.position != oldShipPosition) {
			Bounds generateDistance = new Bounds (Ship.transform.position, Vector3.one * VIEW_DISTANCE);
			foreach (ChunkObject c in StreamSource.GetObjects(generateDistance))
			{
				if (!LiveObjects.Exists(i => i.transform.position == c.Position))
				{
					GameObject GO = PhotonNetwork.InstantiateSceneObject(c.ChunkObjectType, c.Position, c.Rotation,0, null);
					GO.transform.localScale = new Vector3(400,400,400);
					LiveObjects.Add(GO);
					GO.transform.parent = this.transform;
				}
			}
			List<GameObject> temp = LiveObjects;
			foreach (GameObject go in temp.Where(o => !generateDistance.Intersects(new Bounds(o.transform.position,new Vector3(3,3,3))))){
				Debug.Log ("Deleting" + go.transform.position.ToString ());
				temp.Remove (go);
				PhotonNetwork.Destroy (go);
			}
			LiveObjects = temp;
		}
		oldShipPosition = Ship.transform.position;
	}

	void DeleteCO(GameObject go)
	{

	}
}
