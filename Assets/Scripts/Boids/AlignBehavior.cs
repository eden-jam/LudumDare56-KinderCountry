using System.Collections.Generic;
using UnityEngine;

public class AlignBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 align = Vector2.zero;
		float total = 0;
		foreach (Boids other in others)
		{
			if (other == _self)
			{
				continue;
			}
			float perception = 5.0f;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				align += other.Velocity;
				total++;
			}
		}

		if (total > 0)
		{
			align /= total;
			align = align.normalized * _self.MaxSpeed;
			align -= _self.Velocity;
			align = align.normalized * _self.MaxSpeed;
		}
		return align;
	}
}
