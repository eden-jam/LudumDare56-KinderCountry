using System;
using UnityEngine;

[Serializable]
public class CohesionParameters : IBehaviorParameters
{
	public float PerceptionDistance = 5.0f;
	public float FriendlyWeight = 1.0f;
	public float StrangerWeight = 1.0f;
}
