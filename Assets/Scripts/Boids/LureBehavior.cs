using System.Collections.Generic;
using UnityEngine;

public class LureBehavior : IBehavior
{
	public Transform AttractionPoint;
	public LureParameters LureParameters => _parameters as LureParameters;
	private float _timer = 0.0f;

	public void ResetTimer()
	{
		_timer = LureParameters.Duration;
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
		return Average(attractionSteering, total) * _parameters.Weight;
	}
}
