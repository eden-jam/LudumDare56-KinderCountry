
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
	[SerializeField] private Boids _boidPrefab;
	[SerializeField] private int _count = 100;
    private List<Boids> _boids = new List<Boids>();
	[SerializeField] private List<Transform> _fleePoints = new List<Transform>();
	[SerializeField] private Transform _player = null;

	private void Start()
	{
        for (int i = 0; i < _count; i++)
        {
			_boids.Add(Instantiate(_boidPrefab, transform));
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
