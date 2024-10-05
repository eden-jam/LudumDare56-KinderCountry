using UnityEngine;

[CreateAssetMenu()]
public class BoidsParameters : ScriptableObject
{
	public float MaxSpeed = 5.0f;
	public AlignParameters AlignParameters = new AlignParameters();
	public AttractionParameters AttractionParameters = new AttractionParameters();
	public CohesionParameters CohesionParameters = new CohesionParameters();
	public EdgeAvoidParameters EdgeAvoidParameters = new EdgeAvoidParameters();
	public FleeParameters FleeParameters = new FleeParameters();
	public SeperationParameters SeperationParameters = new SeperationParameters();
}
