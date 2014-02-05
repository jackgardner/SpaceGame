/// <summary>
///
/// </summary>
/// <typeparam name="T"></typeparam>
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class Octree<T>
{
	private readonly int _threshold;
	private readonly Node _root;
	
	/// <summary>
	///
	/// </summary>
	/// <param name="min"></param>
	/// <param name="max"></param>
	/// <param name="threshold"></param>
	public Octree(Vector3 min, Vector3 max, int threshold)
	{
		_threshold = threshold;
		_root = new Node(min, max);
	}
	
	/// <summary>
	/// Insert an item into this octree
	/// </summary>
	/// <param name="bounds"></param>
	/// <param name="item"></param>
	public void Insert(Bounds bounds, T item)
	{
		var a = new Member {Bounds = bounds, Value = item};
		_root.Insert(a, _threshold);
	}
	
	/// <summary>
	///
	/// </summary>
	/// <param name="bounds"></param>
	/// <returns></returns>
	public IEnumerable<T> Intersects(Bounds bounds)
	{
		return _root.Intersects(bounds).Select(a => a.Value);
	}
	
	/// <summary>
	///
	/// </summary>
	/// <param name="bounds"></param>
	/// <returns></returns>
	public IEnumerable<T> ContainedBy(Bounds bounds)
	{
		return _root.Intersects(bounds).Where(a => bounds.Intersects(a.Bounds)).Select(a => a.Value);
	}
	
	private class Node
	{
		private readonly List<Member> _items = new List<Member>();
		
		private Bounds _bounds;
		private Node[] _children;
		
		public Node(Vector3 min, Vector3 max)
		{
			_bounds = CreateBounds(min, max);
		}

		Bounds CreateBounds(Vector3 min, Vector3 max)
		{
			return new Bounds(max * 0.5f + min * 0.5f, (max - min) * 0.5f);
		}
		
		private void Split(int splitThreshold)
		{
			_children = new Node[8];
			int childIndex = 0;
			var min = _bounds.min;
			var size = (_bounds.max - _bounds.min) / 2f;
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					for (int z = 0; z < 2; z++)
					{
						var positionOffset = new Vector3(size.x *  x, size.y * y,size.z * z);
						_children[childIndex++] = new Node(min + positionOffset, min + size + positionOffset);
					}
				}
			}
			
			for (int i = _items.Count - 1; i >= 0; i--)
			{
				var item = _items[i];
				
				//Try to insert this item into each child (if successful, it's removed from this node)
				foreach (Node child in _children)
				{
					if (child._bounds.Intersects(item.Bounds))
					{
						child.Insert(item, splitThreshold);
						_items.RemoveAt(i);
						break;
					}
				}
			}
		}
		
		public void Insert(Member m, int splitThreshold)
		{
			if (_children == null)
			{
				_items.Add(m);
				
				if (_items.Count > splitThreshold)
					Split(splitThreshold);
			}
			else
			{
				//Try to put this item into a child node
				foreach (var child in _children)
				{
					if (child._bounds.Intersects(m.Bounds))
					{
						child.Insert(m, splitThreshold);
						return;
					}
				}
				
				//Failed! Can't find a child to contain this, store it here instead
				_items.Add(m);
			}
		}
		
		public IEnumerable<Member> Intersects(Bounds bounds)
		{
			//Select items in this node
			foreach (var member in _items)
			{
				if (member.Bounds.Intersects(bounds))
					yield return member;
			}
			
			//Select items in children
			if (_children != null)
			{
				foreach (var member in _children.Where(c => c._bounds.Intersects(bounds)).SelectMany(c => c.Intersects(bounds)))
				{
					yield return member;
				}
			}
		}
	}
	private struct Member
	{
		public T Value;
		public Bounds Bounds;
	}
}
	
