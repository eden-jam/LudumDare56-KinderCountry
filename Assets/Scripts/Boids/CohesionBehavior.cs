using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class CohesionBehavior : IBehavior
{
	public CohesionParameters CohesionParameters => _parameters as CohesionParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (CohesionParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Profiler.BeginSample(GetType().Name);
		Vector3 cohesion = Vector2.zero;
		int total = 0;
		foreach (Boids other in others)
		{
			if (other == _self || other.HasFinish != _self.HasFinish)
			{
				continue;
			}
			float perception = CohesionParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.sqrMagnitude;
			if (dist < perception * perception)
			{
				float weight = other.Type == _self.Type ? CohesionParameters.FriendlyWeight : CohesionParameters.StrangerWeight;
				cohesion -= diff.normalized * weight;
				total++;
			}
		}

		DebugValue = Average(cohesion, total) * _parameters.Weight;
		Profiler.EndSample();
		return Average(cohesion, total) * _parameters.Weight;
	}
}
