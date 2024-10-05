using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody _rigidbody = null;
    private MeshRenderer _meshRenderer = null;
    [SerializeField] private float _speed = 5.0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

		_rigidbody.linearVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized * _speed;
        _meshRenderer.transform.LookAt(_rigidbody.position + _rigidbody.linearVelocity);
	}
}
