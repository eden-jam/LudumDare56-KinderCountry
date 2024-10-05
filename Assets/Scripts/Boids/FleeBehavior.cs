using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others, in List<Transform> fleePoints)
	{
		Vector3 fleeSteering = Vector3.zero;
		float total = 0;

		foreach (Transform fleePoint in fleePoints)
		{
			float perception = 5.0f;
			Vector3 diff = _self.transform.position - fleePoint.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				fleeSteering += diff.normalized;
				total++;
			}
		}

		if (total > 0)
		{
			fleeSteering /= total;
			fleeSteering = fleeSteering.normalized * _self.MaxSpeed;
			fleeSteering -= _self.Velocity;
			fleeSteering = fleeSteering.normalized * _self.MaxSpeed;
		}
		return fleeSteering;
	}
}
