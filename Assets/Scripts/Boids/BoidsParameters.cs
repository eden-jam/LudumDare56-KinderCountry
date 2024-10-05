using UnityEngine;

[CreateAssetMenu()]
public class BoidsParameters : ScriptableObject
{
	public bool DisplayGizmos = false;
	public Boids BoidPrefab;
	public Type Type = Type.RED;
	public float MaxSpeed = 5.0f;
	public AlignParameters AlignParameters = new AlignParameters();
	public AttractionParameters AttractionParameters = new AttractionParameters();
	public CohesionParameters CohesionParameters = new CohesionParameters();
	public EdgeAvoidParameters EdgeAvoidParameters = new EdgeAvoidParameters();
	public FleeParameters FleeParameters = new FleeParameters();
	public SeperationParameters SeperationParameters = new SeperationParameters();
	public SeperationParameters AntiCollapseParameters = new SeperationParameters() { Weight = 25.0f, PerceptionDistance = 1.0f, FriendlyWeight = 1.0f, StrangerWeight = 1.0f };
}
