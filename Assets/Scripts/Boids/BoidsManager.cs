
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
	[SerializeField] private int _count = 100;
    private List<Boids> _boids = new List<Boids>();
	[SerializeField] private List<Transform> _fleePoints = new List<Transform>();
	[SerializeField] private Transform _player = null;
	[SerializeField] private List<BoidsParameters> _boidsParameters = null;

	private void Start()
	{
        for (int i = 0; i < _count; i++)
        {
			BoidsParameters boidParameters = _boidsParameters[Random.Range(0, _boidsParameters.Count)];
			Boids boid = Instantiate(boidParameters.BoidPrefab, transform);
			boid.Init(boidParameters);
			_boids.Add(boid);
		}
    }

	private void Update()
	{
        foreach (Boids boid in _boids)
        {
			boid.UpdateBoids(_boids, _fleePoints, _player);
		}
    }
}
