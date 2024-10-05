using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class LureBehavior : IBehavior
{
	public Transform AttractionPoint;
	public LureParameters LureParameters => _parameters as LureParameters;
	private float _timer = 0.0f;

	public void StartTimer()
	{
		_timer = LureParameters.Duration;
	}

	public void ResetTimer()
	{
		_timer = 0.0f;
	}

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (LureParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}

		if (_timer <= 0.0f)
		{
			return Vector3.zero;
		}
		else
		{
			_timer -= Time.deltaTime;
		}
		Profiler.BeginSample(GetType().Name);

		Vector3 attractionSteering = Vector3.zero;
		int total = 0;

		float perception = LureParameters.PerceptionDistance;
		Vector3 diff = _self.transform.position - AttractionPoint.transform.position;
		float dist = diff.magnitude;
		if (dist < perception)
		{
			attractionSteering -= diff.normalized;
			total++;
		}

		DebugValue = Average(attractionSteering, total) * _parameters.Weight;
		Profiler.EndSample();
		return Average(attractionSteering, total) * _parameters.Weight;
	}
}
