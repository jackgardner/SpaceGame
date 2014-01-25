using System.Collections.Generic;
using UnityEngine;

public class InMemoryWorldStream : IWorldStreamSource
{
	private Octree<ChunkObject> octree = new Octree<ChunkObject>(new Vector3(-SIZE_OF_SOLAR_SYSTEM / 2), new Vector3(SIZE_OF_SOLAR_SYSTEM / 2));
	
	public InMemoryWorldStream(int worldSeed)
	{
		for (var i = 0; i < 100000; i++) {
			//Generate a chunk object within the solar system
			ChunkObject a;
			octree.Insert(a.Position, a);
		}
	}
	
	public IEnumerable<ChunkObject> GetObjects(Bounds bounds)
	{
		return octree.InRange(bounds);
	}
}