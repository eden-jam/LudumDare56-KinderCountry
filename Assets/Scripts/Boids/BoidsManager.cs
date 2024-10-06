
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
	[SerializeField][Range(0,1)] private float maxAudioTriggerProbability = 0.0f;
	[SerializeField] private float _audioTimeInterval = 6.0f;
	[SerializeField] private int _minCount = 50;
	private int _leavingCount = 100;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Debug.Assert(SoundManager.Instance != null, "Add the sound manager to the scene if you want to hear boids sounds !");

		for (int i = 0; i < _count; i++)
        {
			BoidsParameters boidParameters = _boidsParameters[Random.Range(0, _boidsParameters.Count)];
			Vector3 boidsPosition = new Vector3(Random.Range(-50.0f, 50.0f), 0.0f, Random.Range(-50.0f, 50.0f)) + transform.position;
			Boids boid = Instantiate(boidParameters.BoidPrefab, boidsPosition, Quaternion.identity, transform);
			boid.Init(boidParameters, Random.Range(0.0f, maxAudioTriggerProbability), _audioTimeInterval);
			_boids.Add(boid);
			
			if (SoundManager.Instance != null)
			{
				SoundManager.Instance.RegisterAudioSource(boid.AudioSource);
			}
			
		}
    }

	private void Update()
	{
        foreach (Boids boid in _boids)
        {
			boid.UpdateBoids(_boids, _fleePoints, _player);

			if (SoundManager.Instance != null)
			{
				boid.TriggerSoundIfNeeded();
			}
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

		if (_leavingCount <= _minCount)
		{
			FinishManager.Instance.Finish();
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
