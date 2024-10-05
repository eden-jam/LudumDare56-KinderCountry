using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttractionBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others, in Transform attractionPoint)
	{
		Vector3 attractionSteering = Vector3.zero;
		float total = 0;

		float perception = 30.0f;
		Vector3 diff = _self.transform.position - attractionPoint.transform.position;
		float dist = diff.magnitude;
		if (dist < perception)
		{
			attractionSteering -= diff.normalized;
			total++;
		}

		if (total > 0)
		{
			attractionSteering /= total;
			attractionSteering = attractionSteering.normalized * _self.MaxSpeed;
			attractionSteering -= _self.Velocity;
			attractionSteering = attractionSteering.normalized * _self.MaxSpeed;
		}

		return attractionSteering;
	}
}
