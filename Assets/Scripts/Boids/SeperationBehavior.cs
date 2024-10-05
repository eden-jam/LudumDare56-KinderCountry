using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class SeperationBehavior : IBehavior
{
	public SeperationParameters SeperationParameters => _parameters as SeperationParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (SeperationParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Profiler.BeginSample(GetType().Name);
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
			float dist = diff.sqrMagnitude;
			if (dist < perception * perception)
			{
				float weight = other.Type == _self.Type ? SeperationParameters.FriendlyWeight : SeperationParameters.StrangerWeight;
				separation += diff.normalized * weight;
				total++;
			}
		}

		DebugValue = Average(separation, total) * _parameters.Weight;
		Profiler.EndSample();
		return Average(separation, total) * _parameters.Weight;
	}
}
