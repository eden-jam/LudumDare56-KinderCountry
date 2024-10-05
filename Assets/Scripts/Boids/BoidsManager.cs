
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
	public static BoidsManager Instance = null;

	[SerializeField] private int _count = 100;
    private List<Boids> _boids = new List<Boids>();
	[SerializeField] private List<Transform> _fleePoints = new List<Transform>();
	[SerializeField] private Transform _player = null;
	[SerializeField] private List<BoidsParameters> _boidsParameters = null;
	[SerializeField] private GameObject _fleePrefab = null;
	[SerializeField] private Transform _fleeParent = null;
	private int _leavingCount = 100;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		for (int i = 0; i < _count; i++)
        {
			BoidsParameters boidParameters = _boidsParameters[Random.Range(0, _boidsParameters.Count)];
			Vector3 boidsPosition = new Vector3(Random.Range(-90.0f, 100.0f), 0.0f, Random.Range(-90.0f, 90.0f));
			Boids boid = Instantiate(boidParameters.BoidPrefab, boidsPosition, Quaternion.identity, transform);
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

	public int GetBoidsCount()
	{
		return _leavingCount;
	}

	public void UpdateCount()
	{
		_leavingCount = _boids.Count;

		foreach (Boids boid in _boids)
        {
			if (boid.HasFinish)
			{
				_leavingCount--;
			}
		}
    }

	public void Lure()
	{
		foreach (Boids boid in _boids)
		{
			boid.Lure(_player);
		}
	}

	public void Cry()
	{
		foreach (Boids boid in _boids)
		{
			boid.Cry(_player);
		}
	}

	public void SpawnFlee(Vector3 position)
	{
		_fleePoints.Add(Instantiate(_fleePrefab, position, Quaternion.identity, _fleeParent).transform);
	}
}
