using System;
using UnityEngine;

[Serializable]
public class EdgeAvoidParameters : IBehaviorParameters
{
	[NonSerialized] public Vector2 MinPosition;
	[NonSerialized] public Vector2 MaxPosition;
}
