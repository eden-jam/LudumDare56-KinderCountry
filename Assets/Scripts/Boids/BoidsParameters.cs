using UnityEngine;

[CreateAssetMenu()]
public class BoidsParameters : ScriptableObject
{
	public bool DisplayGizmos = false;
	public bool DisplayDistance = false;
	public bool Normalize = false;
	public Boids BoidPrefab;
	public Type Type = Type.RED;
	public float MaxSpeed = 5.0f;
	public float KeepVelocity = 1.0f;
	public AlignParameters AlignParameters = new AlignParameters();
	public AttractionParameters AttractionParameters = new AttractionParameters();
	public CohesionParameters CohesionParameters = new CohesionParameters();
	public EdgeAvoidParameters EdgeAvoidParameters = new EdgeAvoidParameters() { MinPosition = new Vector2(-90.0f, -105.0f), MaxPosition = new Vector2(100.0f, 90.0f) };
	public FleeParameters FleeParameters = new FleeParameters();
	public SeperationParameters SeperationParameters = new SeperationParameters();
	public SeperationParameters AntiCollapseParameters = new SeperationParameters() { Weight = 25.0f, PerceptionDistance = 1.0f, FriendlyWeight = 1.0f, StrangerWeight = 1.0f };
	public LureParameters LureParameters = new LureParameters();
	public CryParameters CryParameters = new CryParameters();
	public AttractionParameters PuddleAttraction = new AttractionParameters() { Weight = 20.0f, PerceptionDistance = 50.0f };
}
