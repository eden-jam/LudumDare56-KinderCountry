using System.Collections.Generic;
using UnityEngine;

public class EdgeAvoidBehavior
{
	private Boids _self = null;

	public void Init(Boids self)
	{
		_self = self;
	}

	public Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 steering = Vector2.zero;
		float total = 0;

		Vector2 wallOffset = new Vector2(50.0f, 50.0f);

		if (_self.transform.position.x < -wallOffset.x)
		{
			Vector3 diff = new Vector3(-wallOffset.x - _self.transform.position.x, 0.0f, 0.0f);
			steering += diff;
			total++;
		}

		if (_self.transform.position.z < -wallOffset.y)
		{
			Vector3 diff = new Vector3(0.0f, 0.0f, -wallOffset.y - _self.transform.position.z);
			steering += diff;
			total++;
		}

		if (_self.transform.position.x > wallOffset.x)
		{
			Vector3 diff = new Vector3(wallOffset.x - _self.transform.position.x, 0.0f, 0.0f);
			steering += diff;
			total++;
		}

		if (_self.transform.position.z > wallOffset.x)
		{
			Vector3 diff = new Vector3(0.0f, 0.0f, wallOffset.y - _self.transform.position.z);
			steering += diff;
			total++;
		}

		if (total > 0)
		{
			steering /= total;
			steering = steering.normalized * _self.MaxSpeed;
			steering -= _self.Velocity;
			steering = steering.normalized * _self.MaxSpeed;
		}
		return steering;
	}
}
