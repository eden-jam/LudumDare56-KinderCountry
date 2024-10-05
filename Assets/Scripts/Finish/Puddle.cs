using UnityEngine;

public class Puddle : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Boids>(out Boids boids))
		{
			boids.Finish(transform);
			BoidsManager.Instance.UpdateCount();
		}
	}
}
