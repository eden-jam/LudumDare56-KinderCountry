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
    private Rigidbody _rigidbody = null;
    [SerializeField] private Transform _meshRenderer = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private AudioSource _audioSource = null;

    private SeperationBehavior _seperationBehavior = new SeperationBehavior();
    private SeperationBehavior _antiCollapseBehavior = new SeperationBehavior();
    private EdgeAvoidBehavior _edgeAvoidBehavior = new EdgeAvoidBehavior();
    private CohesionBehavior _cohesionBehavior = new CohesionBehavior();
    private AlignBehavior _alignBehavior = new AlignBehavior();
    private FleeBehavior _fleeBehavior = new FleeBehavior();
    private AttractionBehavior _attractionBehavior = new AttractionBehavior();
    private LureBehavior _lureBehavior = new LureBehavior();
    private CryBehavior _cryBehavior = new CryBehavior();
    private AttractionBehavior _puddleAttraction = new AttractionBehavior();

    private BoidsParameters _boidsParameters = null;

	private bool _hasFinish = false;
	private float _triggerSoundProbability = 0.0f;
	private float _audioTimeInterval = 0.0f;
	private float _lastTimePlayed = 0.0f;

	public Vector3 Velocity
    {
        get { return _rigidbody.linearVelocity; }
        set 
		{
			_rigidbody.linearVelocity = new Vector3(value.x, _rigidbody.linearVelocity.y, value.z);
		}
	}

	public float MaxSpeed
	{
		get { return _boidsParameters.MaxSpeed; }
	}

	public Type Type
	{
		get { return _boidsParameters.Type; }
	}

	public bool HasFinish
	{
		get { return _hasFinish; }
	}

	public AudioSource AudioSource
	{
		get { return _audioSource; }
	}

	private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		_rigidbody.linearVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized * _boidsParameters.MaxSpeed;

		// Do we want to listen to events for some reasons,
		_animator.fireEvents = false;
		_lastTimePlayed = Random.Range(0.0f, 20.0f);

		_meshRenderer.transform.LookAt(_rigidbody.position + _rigidbody.linearVelocity);
	}

    public void Init(BoidsParameters boidsParameters, float triggerSoundProbability, float audioTimeInterval)
	{
        _boidsParameters = boidsParameters;
		_triggerSoundProbability = triggerSoundProbability;
		_audioTimeInterval = audioTimeInterval;
		_alignBehavior.Init(this, _boidsParameters.AlignParameters);
		_attractionBehavior.Init(this, _boidsParameters.AttractionParameters);
		_cohesionBehavior.Init(this, _boidsParameters.CohesionParameters);
		_edgeAvoidBehavior.Init(this, _boidsParameters.EdgeAvoidParameters);
		_fleeBehavior.Init(this, _boidsParameters.FleeParameters);
		_seperationBehavior.Init(this, _boidsParameters.SeperationParameters);
		_antiCollapseBehavior.Init(this, _boidsParameters.AntiCollapseParameters);
		_lureBehavior.Init(this, _boidsParameters.LureParameters);
		_cryBehavior.Init(this, _boidsParameters.CryParameters);
		_puddleAttraction.Init(this, _boidsParameters.PuddleAttraction);
	}

	public void Lure(Transform transform)
	{
		_lureBehavior.AttractionPoint = transform;
		_lureBehavior.StartTimer();
		_cryBehavior.ResetTimer();
	}

	public void Cry(Transform transform)
	{
		_cryBehavior.AttractionPoint = transform;
		_cryBehavior.StartTimer();
		_lureBehavior.ResetTimer();
	}

	public void UpdateBoids(in List<Boids> others, in List<Transform> fleePoint, in Transform attractionPoint)
	{
		if (_hasFinish == false)
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
			Vector3 lure = _lureBehavior.UpdateBoids(others);
			Vector3 cry = _cryBehavior.UpdateBoids(others);

			Velocity += Velocity * _boidsParameters.KeepVelocity * Time.deltaTime;
			Velocity += align * Time.deltaTime;
			Velocity += attraction * Time.deltaTime;
			Velocity += cohesion * Time.deltaTime;
			Velocity += edgeAvoid * Time.deltaTime;
			Velocity += flee * Time.deltaTime;
			Velocity += separation * Time.deltaTime;
			Velocity += antiCollapse * Time.deltaTime;
			Velocity += lure * Time.deltaTime;
			Velocity += cry * Time.deltaTime;
		}
		else
		{
			Vector3 attraction = _puddleAttraction.UpdateBoids(others);
			Vector3 antiCollapse = _antiCollapseBehavior.UpdateBoids(others);

			Velocity += attraction * Time.deltaTime;
			Velocity += antiCollapse * Time.deltaTime;
		}

		_animator.SetBool("IsControlled", _attractionBehavior.IsFollowingPlayer);

		if (Velocity.magnitude > _boidsParameters.MaxSpeed || _boidsParameters.Normalize)
		{
			Velocity = Velocity.normalized * _boidsParameters.MaxSpeed;
		}
	}

	public void TriggerSoundIfNeeded()
	{
		// Play a sound if the random is above the probability set.
		_lastTimePlayed += Time.deltaTime;
		float random = Random.Range(0.0f, 1.0f);
		if (random < _triggerSoundProbability && _lastTimePlayed >= _audioTimeInterval)
		{
			SoundManager.Instance.PlayAudioSource(AudioSource);
			_lastTimePlayed = 0.0f;
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
		Gizmos.color = Color.black;
		Gizmos.DrawRay(transform.position, _lureBehavior.DebugValue);
		Gizmos.color = Color.white;
		Gizmos.DrawRay(transform.position, _cryBehavior.DebugValue);
	}

	public void Finish(Transform finish)
	{
		_hasFinish = true;
		_puddleAttraction.AttractionPoint = finish;
	}
}
