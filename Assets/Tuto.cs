using UnityEngine;

public class Tuto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void Play()
	{
		Time.timeScale = 1.0f;
        Destroy(gameObject);
	}
}
