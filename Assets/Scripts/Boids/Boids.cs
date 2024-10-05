using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody _rigidbody = null;
    private MeshRenderer _meshRenderer = null;
    [SerializeField] private float _maxSpeed = 5.0f;

    private SeperationBehavior _seperationBehavior = new SeperationBehavior();
    private EdgeAvoidBehavior _edgeAvoidBehavior = new EdgeAvoidBehavior();
    private CohesionBehavior _cohesionBehavior = new CohesionBehavior();
    private AlignBehavior _alignBehavior = new AlignBehavior();
    private FleeBehavior _fleeBehavior = new FleeBehavior();

    public Vector3 Velocity
    {
        get { return _rigidbody.linearVelocity; }
        set { _rigidbody.linearVelocity = value; }
	}

	public float MaxSpeed
	{
		get { return _maxSpeed; }
	}

	private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

		_rigidbody.linearVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized * _maxSpeed;
        _meshRenderer.transform.LookAt(_rigidbody.position + _rigidbody.linearVelocity);
        _seperationBehavior.Init(this);
		_edgeAvoidBehavior.Init(this);
		_cohesionBehavior.Init(this);
		_alignBehavior.Init(this);
		_fleeBehavior.Init(this);
	}

    public void UpdateBoids(in List<Boids> others, in List<Transform> fleePoint)
    {
        Vector3 separation = _seperationBehavior.UpdateBoids(others);
        Vector3 edgeAvoid = _edgeAvoidBehavior.UpdateBoids(others);
        Vector3 cohesion = _cohesionBehavior.UpdateBoids(others);
        Vector3 align = _alignBehavior.UpdateBoids(others);
        Vector3 flee = _fleeBehavior.UpdateBoids(others, fleePoint);
		Velocity += flee * 5.0f * Time.deltaTime;
		Velocity += align * 1.0f * Time.deltaTime;
		Velocity += cohesion * 0.5f * Time.deltaTime;
		Velocity += separation * 3.0f * Time.deltaTime;
		Velocity += edgeAvoid * 5.0f * Time.deltaTime;
		if (Velocity.magnitude > _maxSpeed)
		{
			Velocity = Velocity.normalized * _maxSpeed;
		}
	}
}
