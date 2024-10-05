using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody _rigidbody = null;
    private MeshRenderer _meshRenderer = null;
    [SerializeField] private float _maxSpeed = 5.0f;

    private SeperationBehavior _seperationBehavior = new SeperationBehavior();

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
	}

    public void UpdateBoids(in List<Boids> others)
    {
        Vector3 separation = _seperationBehavior.UpdateBoids(others);
        Velocity += separation * Time.deltaTime;
		if (Velocity.magnitude > _maxSpeed)
		{
			Velocity = Velocity.normalized * _maxSpeed;
		}
	}
}
