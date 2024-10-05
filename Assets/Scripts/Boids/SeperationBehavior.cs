using System.Collections.Generic;
using UnityEngine;

public class SeperationBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 separation = Vector2.zero;
		float total = 0;
        foreach (Boids other in others)
        {
			if (other == _self)
			{
				continue;
			}
			float perception = 2.0f;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				separation += diff.normalized;
				total++;
			}
		}

		if (total > 0)
		{
			separation /= total;
			separation = separation.normalized * _self.MaxSpeed;
			separation -= _self.Velocity;
			separation = separation.normalized * _self.MaxSpeed;
		}
		return separation;
	}
}
