using System.Collections.Generic;
using UnityEngine;

public abstract class IBehavior
{
	protected Boids _self = null;
	protected IBehaviorParameters _parameters = null;
	public Vector3 DebugValue = Vector3.zero;

	public void Init(Boids self, IBehaviorParameters behaviorParameters)
	{
		_self = self;
		_parameters = behaviorParameters;
	}

	protected Vector3 Average(Vector3 steering, int total)
	{
		steering.y = 0;
		if (total > 0)
		{
			steering /= total;
			steering = steering.normalized * _self.MaxSpeed;
			steering -= _self.Velocity;
			steering = steering.normalized * _self.MaxSpeed;
		}
		return steering;
	}

	public abstract Vector3 UpdateBoids(in List<Boids> others);
}
