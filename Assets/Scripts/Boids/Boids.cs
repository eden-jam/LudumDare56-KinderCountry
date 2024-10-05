using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
	RED,
	GREEN,
	BLUE
}

public class Boids : MonoBehaviour
{
	public int Life = 100;

    private Rigidbody _rigidbody = null;
    [SerializeField] private Transform _meshRenderer = null;

    private SeperationBehavior _seperationBehavior = new SeperationBehavior();
    private SeperationBehavior _antiCollapseBehavior = new SeperationBehavior();
    private EdgeAvoidBehavior _edgeAvoidBehavior = new EdgeAvoidBehavior();
    private CohesionBehavior _cohesionBehavior = new CohesionBehavior();
    private AlignBehavior _alignBehavior = new AlignBehavior();
    private FleeBehavior _fleeBehavior = new FleeBehavior();
    private AttractionBehavior _attractionBehavior = new AttractionBehavior();

    private BoidsParameters _boidsParameters = null;

    public Vector3 Velocity
    {
        get { return _rigidbody.linearVelocity; }
        set { _rigidbody.linearVelocity = value; }
	}

	public float MaxSpeed
	{
		get { return _boidsParameters.MaxSpeed; }
	}

	public Type Type
	{
		get { return _boidsParameters.Type; }
	}

	private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		_rigidbody.linearVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));


		_meshRenderer.transform.LookAt(_rigidbody.position + _rigidbody.linearVelocity);
	}

    public void Init(BoidsParameters boidsParameters)
	{
        _boidsParameters = boidsParameters;
		_alignBehavior.Init(this, _boidsParameters.AlignParameters);
		_attractionBehavior.Init(this, _boidsParameters.AttractionParameters);
		_cohesionBehavior.Init(this, _boidsParameters.CohesionParameters);
		_edgeAvoidBehavior.Init(this, _boidsParameters.EdgeAvoidParameters);
		_fleeBehavior.Init(this, _boidsParameters.FleeParameters);
		_seperationBehavior.Init(this, _boidsParameters.SeperationParameters);
		_antiCollapseBehavior.Init(this, _boidsParameters.AntiCollapseParameters);
	}

    public void UpdateBoids(in List<Boids> others, in List<Transform> fleePoint, in Transform attractionPoint)
    {
        Vector3 align = _alignBehavior.UpdateBoids(others);
		_attractionBehavior.AttractionPoint = attractionPoint;
        Vector3 attraction = _attractionBehavior.UpdateBoids(others);
        Vector3 cohesion = _cohesionBehavior.UpdateBoids(others);
        Vector3 edgeAvoid = _edgeAvoidBehavior.UpdateBoids(others);
		_fleeBehavior.FleePoints = fleePoint;
		Vector3 flee = _fleeBehavior.UpdateBoids(others);
        Vector3 separation = _seperationBehavior.UpdateBoids(others);
        Vector3 antiCollapse = _antiCollapseBehavior.UpdateBoids(others);

		Velocity += align * Time.deltaTime;
		Velocity += attraction * Time.deltaTime;
		Velocity += cohesion * Time.deltaTime;
		Velocity += edgeAvoid * Time.deltaTime;
		Velocity += flee * Time.deltaTime;
		Velocity += separation * Time.deltaTime;
		Velocity += antiCollapse * Time.deltaTime;

		if (Velocity.magnitude > _boidsParameters.MaxSpeed)
		{
			Velocity = Velocity.normalized * _boidsParameters.MaxSpeed;
		}
	}

	private void OnDrawGizmos()
	{
		if (_boidsParameters.DisplayGizmos == false)
		{
			return;
		}
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, _alignBehavior.DebugValue);
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, _attractionBehavior.DebugValue);
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, _cohesionBehavior.DebugValue);
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, _edgeAvoidBehavior.DebugValue);
		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(transform.position, _fleeBehavior.DebugValue);
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay(transform.position, _seperationBehavior.DebugValue);
	}
}
