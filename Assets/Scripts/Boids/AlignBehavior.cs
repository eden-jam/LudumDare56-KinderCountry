using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class AlignBehavior : IBehavior
{
	public AlignParameters AlignParameters => _parameters as AlignParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (AlignParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Profiler.BeginSample(GetType().Name);
		Vector3 align = Vector2.zero;
		int total = 0;
		foreach (Boids other in others)
		{
			if (other == _self)
			{
				continue;
			}
			float perception = AlignParameters.PerceptionDistance;
			Vector3 diff = _self.transform.position - other.transform.position;
			float dist = diff.sqrMagnitude;
			if (dist < perception * perception)
			{
				float weight = other.Type == _self.Type ? AlignParameters.FriendlyWeight : AlignParameters.StrangerWeight;
				align += other.Velocity * weight;
				total++;
			}
		}

		DebugValue = Average(align, total) * _parameters.Weight;
		Profiler.EndSample();
		return Average(align, total) * AlignParameters.Weight;
	}
}
