using System.Collections.Generic;
using UnityEngine;

public class AttractionBehavior : IBehavior
{
	private bool _isFollowingPlayer = false;

	public Transform AttractionPoint;
	public AttractionParameters AttractionParameters => _parameters as AttractionParameters;
	public bool IsFollowingPlayer => _isFollowingPlayer;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		if (AttractionParameters.Weight == 0.0f)
		{
			return Vector3.zero;
		}
		Vector3 attractionSteering = Vector3.zero;
		int total = 0;

		float perception = AttractionParameters.PerceptionDistance;
		Vector3 diff = _self.transform.position - AttractionPoint.transform.position;
		float dist = diff.magnitude;
		if (_isFollowingPlayer = dist < perception)
		{
			attractionSteering -= diff.normalized;
			total++;
		}

		DebugValue = Average(attractionSteering, total) * _parameters.Weight;
		return Average(attractionSteering, total) * _parameters.Weight;
	}
}
