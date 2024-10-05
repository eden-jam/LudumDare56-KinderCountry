using System.Collections.Generic;
using UnityEngine;

public class EdgeAvoidBehavior : IBehavior
{
	public EdgeAvoidParameters EdgeAvoidParameters => _parameters as EdgeAvoidParameters;

	public override Vector3 UpdateBoids(in List<Boids> others)
	{
		Vector3 steering = Vector2.zero;
		int total = 0;

		if (_self.transform.position.x < EdgeAvoidParameters.MinPosition.x)
		{
			Vector3 diff = new Vector3(EdgeAvoidParameters.MinPosition.x - _self.transform.position.x, 0.0f, 0.0f);
			steering += diff;
			total++;
		}

		if (_self.transform.position.z < EdgeAvoidParameters.MinPosition.y)
		{
			Vector3 diff = new Vector3(0.0f, 0.0f, EdgeAvoidParameters.MinPosition.y - _self.transform.position.z);
			steering += diff;
			total++;
		}

		if (_self.transform.position.x > EdgeAvoidParameters.MaxPosition.x)
		{
			Vector3 diff = new Vector3(EdgeAvoidParameters.MaxPosition.x - _self.transform.position.x, 0.0f, 0.0f);
			steering += diff;
			total++;
		}

		if (_self.transform.position.z > EdgeAvoidParameters.MaxPosition.y)
		{
			Vector3 diff = new Vector3(0.0f, 0.0f, EdgeAvoidParameters.MaxPosition.y - _self.transform.position.z);
			steering += diff;
			total++;
		}

		return Average(steering, total) * _parameters.Weight;
	}
}
