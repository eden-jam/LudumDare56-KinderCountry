using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior : IBehavior
{
	public CohesionParameters CohesionParameters => _parameters as CohesionParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 cohesion = Vector2.zero;
		int total = 0;
		foreach (Boids other in others)
		{
			if (other == _self)
			{
				continue;
			}
			float perception = CohesionParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				cohesion -= diff.normalized;
				total++;
			}
		}

		return Average(cohesion, total) * _parameters.Weight;
	}
}
