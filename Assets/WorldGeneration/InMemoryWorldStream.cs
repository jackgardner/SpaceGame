using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InMemoryWorldStream : IWorldStreamSource
{
	private Octree<ChunkObject> octree = new Octree<ChunkObject>(Vector3.one * (-50000 / 2),Vector3.one * (50000 / 2), 100);
	
	public InMemoryWorldStream(int worldSeed)
	{
		for (var i = 0; i < 100000; i++) {
			float xLocation = UnityEngine.Random.Range(-25000, 25000);
			float yLocation = UnityEngine.Random.Range(-25000, 25000);
			float zLocation = UnityEngine.Random.Range(-25000, 25000);
				
				ChunkObject newobject = new ChunkObject(){
					Position = new Vector3(xLocation, yLocation, zLocation), 
					Scale = Vector3.one, 
					Rotation = Quaternion.identity, 
					ChunkObjectType = "Asteroid"};

			octree.Insert(new Bounds(newobject.Position, newobject.Scale), newobject);
		}
	}
	
	public List<ChunkObject> GetObjects(Bounds bounds)
	{
		return octree.Intersects(bounds).ToList();
	}
}