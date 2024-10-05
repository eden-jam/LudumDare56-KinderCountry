using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior : IBehavior
{
	public CohesionParameters CohesionParameters => _parameters as CohesionParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (CohesionParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Vector3 cohesion = Vector2.zero;
		int total = 0;
		foreach (Boids other in others)
		{
			if (other == _self)
			{
				continue;
			}
			float weight = other.Type == _self.Type ? CohesionParameters.FriendlyWeight : CohesionParameters.StrangerWeight;
			float perception = CohesionParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				cohesion -= diff.normalized * weight;
				total++;
			}
		}

		return Average(cohesion, total) * _parameters.Weight;
	}
}
