using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 cohesion = Vector2.zero;
		float total = 0;
		foreach (Boids other in others)
		{
			if (other == _self)
			{
				continue;
			}
			float perception = 20.0f;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				cohesion -= diff.normalized;
				total++;
			}
		}

		if (total > 0)
		{
			cohesion /= total;
			cohesion = cohesion.normalized * _self.MaxSpeed;
			cohesion -= _self.Velocity;
			cohesion = cohesion.normalized * _self.MaxSpeed;
		}
		return cohesion;
	}
}
