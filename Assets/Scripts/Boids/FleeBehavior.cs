using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior : IBehavior
{
	public FleeParameters FleeParameters => _parameters as FleeParameters;

	public List<Transform> FleePoints;
	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (FleeParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Vector3 fleeSteering = Vector3.zero;
		int total = 0;

		foreach (Transform fleePoint in FleePoints)
		{
			float perception = FleeParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - fleePoint.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				fleeSteering += diff.normalized;
				total++;
			}
		}

		return Average(fleeSteering, total);
	}
}
