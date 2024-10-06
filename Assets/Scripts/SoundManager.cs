using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField] private int MAX_PARALLEL_AUDIOS = 10;
    private List<AudioSource> _audioSources;

    private int _nbAudioActive = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}

		Instance = this;
        DontDestroyOnLoad(this);

		_audioSources = new List<AudioSource>(MAX_PARALLEL_AUDIOS);
	}

    public void RegisterAudioSource(AudioSource audioSource)
    {
        _audioSources.Add(audioSource);
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        if (_nbAudioActive < MAX_PARALLEL_AUDIOS)
        {
            audioSource.Play();
			_nbAudioActive++;
		}
    }

    public void Update()
	{
        _nbAudioActive = 0;
		for (int i = 0; i < _audioSources.Count; i++)
        {
            if (_audioSources[i].isPlaying)
            {
				_nbAudioActive++;
			}
		}
	}

	public void OnDestroy()
	{
        _audioSources.Clear();
        _nbAudioActive = 0;
	}
}
