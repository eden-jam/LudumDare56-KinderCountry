using System.Collections.Generic;
using UnityEngine;

public class SeperationBehavior : IBehavior
{
	public SeperationParameters SeperationParameters => _parameters as SeperationParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 separation = Vector2.zero;
		int total = 0;
        foreach (Boids other in others)
        {
			if (other == _self)
			{
				continue;
			}
			float perception = SeperationParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.magnitude;
			if (dist < perception)
			{
				separation += diff.normalized;
				total++;
			}
		}

		return Average(separation, total) * _parameters.Weight;
	}
}
